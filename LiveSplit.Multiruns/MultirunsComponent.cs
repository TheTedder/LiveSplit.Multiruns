using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using LiveSplit.Model.RunSavers;
using LiveSplit.Model.RunFactories;
using LiveSplit.Model.Comparisons;
using System.Windows.Forms;

namespace LiveSplit.Multiruns
{
    public class MultirunsComponent : LogicComponent
    {
        private readonly MultirunsSettings Settings;
        private readonly LiveSplitState State;
        private readonly TimerModel Timer;
        public int Index { get; private set; } = 0;
        private bool DoReset = false;
        private List<IRun> PendingRuns;

        public MultirunsComponent(LiveSplitState s)
        {
            PendingRuns = new List<IRun>(MultirunsSettings.rows);
            State = s;
            Timer = new TimerModel { CurrentState = State };
            Settings = new MultirunsSettings(this);
            State.OnSplit += State_OnSplit;
            State.OnReset += State_OnReset;
            Settings[0] = State.Run.FilePath;
        }

        private void State_OnReset(object sender, TimerPhase value)
        {
            if (Index > 0 && DoReset)
            {
                SaveRuns();
                LoadSplits(0);
                PendingRuns.Clear();
                Index = 0;
                DoReset = false;
            }
        }

        public void SaveRuns()
        {
            if(PendingRuns.Count > 0)
            {
                var owner = (IWin32Window)State.Form.GetContainerControl();
                var runsaver = new XMLRunSaver();
                for (int i = 0; i < PendingRuns.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Settings[i]) && i!= Index)
                    {
                        State.Run = PendingRuns[i];
                        string text = "Save this " + PendingRuns[i].GameName + " run?";
                        if (MessageBox.Show(owner, text, "Livesplit", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            runsaver.Save(PendingRuns[i], Settings.Save(i));
                        }
                    }
                }
            }
        }

        public void TimerUpdate()
        {
            Timer.UpdateAttemptHistory();
            Timer.UpdateBestSegments();
            Timer.UpdatePBSplits();
            Timer.UpdateSegmentHistory();
        }

        private void State_OnSplit(object sender, EventArgs e)
        {
            if (State.CurrentPhase == TimerPhase.Ended && Settings.On)
            {
                if (LoadSplits(Index + 1,true))
                {
                    Index++;
                    DoReset = false;
                    Timer.Reset();
                    DoReset = true;
                    Timer.Start();
                }
            }
        }

        public bool LoadSplits(int i, bool saveRun = false)
        {
            if (!string.IsNullOrEmpty(Settings[i]))
            {
                try
                {
                    var compgenfact = new StandardComparisonGeneratorsFactory();
                    var runfact = new XMLRunFactory(Settings.Open(i), Settings[i]);
                    var run = runfact.Create(compgenfact);
                    if (saveRun)
                    {
                        TimerUpdate();
                        PendingRuns.Add(State.Run);
                    }
                    State.Run = run;
                    //try
                    //{
                    //    State.Run.AutoSplitter = AutoSplitterFactory.Instance.AutoSplitters[State.Run.GameName.ToLower()];
                    //}
                    //catch (KeyNotFoundException)
                    //{
                        State.Run.AutoSplitter = AutoSplitterFactory.Instance.Create(State.Run.GameName);
                    //}
                    
                    return true;
                }
                catch (IndexOutOfRangeException)
                {
                    Debug.WriteLine("Error: tried to load splits file #"+i+" when there are only "+MultirunsSettings.rows + " avaiable.");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public override string ComponentName => "Multiruns";

        public override void Dispose()
        {
            State.OnSplit -= State_OnSplit;
            State.OnReset -= State_OnReset;
        }

        public override XmlNode GetSettings(XmlDocument document)
        {
            XmlElement elem = document.CreateElement("Settings");
            elem.AppendChild(SettingsHelper.ToElement(document, "Version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3)));
            elem.AppendChild(SettingsHelper.ToElement(document,"Enabled",Settings.On));
            var splitsElem = elem.AppendChild(document.CreateElement("Splits"));

            for (int i = 0; i < MultirunsSettings.rows; i++)
            {
                var splitElem = (XmlElement)splitsElem.AppendChild(document.CreateElement("File"));
                if (!string.IsNullOrEmpty(Settings[i])){
                    splitElem.InnerText = Settings[i];
                }
            }

            return elem;
        }

        public override System.Windows.Forms.Control GetSettingsControl(LayoutMode mode)
        {
            return Settings;
        }

        public override void SetSettings(XmlNode settings)
        {
            var elem = (XmlElement) settings;
            Settings.On = SettingsHelper.ParseBool(elem["Enabled"], false);
            var splitsElem = elem["Splits"];

            if (splitsElem != null)
            {
                for (int i = 0; i < MultirunsSettings.rows; i++)
                {
                    Settings[i] = SettingsHelper.ParseString((XmlElement)splitsElem.ChildNodes.Item(i), string.Empty);
                }
            }
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {

        }
    }
}
