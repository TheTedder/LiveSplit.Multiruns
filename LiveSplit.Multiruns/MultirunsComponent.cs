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
        private LiveSplitState State;
        private readonly TimerModel Timer;
        public int Index { get; private set; } = 0;
        private bool DoReset = true;
        private bool DoStart = true;

        private readonly List<IRun> PendingRuns;

        public MultirunsComponent(LiveSplitState s)
        {
            PendingRuns = new List<IRun>();
            State = s;
            Timer = new TimerModel { CurrentState = State };
            Settings = new MultirunsSettings(this);
            State.OnSplit += State_OnSplit;
            State.OnReset += State_OnReset;
            State.OnStart += State_OnStart;
            Settings[0] = State.Run.FilePath;
        }

        private void State_OnStart(object sender, EventArgs e)
        {
            if (DoStart && Settings.On)
            {
                Timer.Pause();
                var hasSubsplits = false;
                for (int i = 0; i < Settings.Count; i++)
                {
                    try
                    {
                        IRun run;
                        if (i != Index)
                        {
                            var compgentfact = new StandardComparisonGeneratorsFactory();
                            var runfact = new StandardFormatsRunFactory(Settings.Open(i), Settings[i]);
                            run = runfact.Create(compgentfact);
                        }
                        else
                        {
                            run = State.Run;
                        }

                        if (run.Count(iseg => iseg.Name[0] == '-') > 0)
                        {
                            hasSubsplits = true;
                        }
                    }
                    catch (ArgumentNullException) { }
                }

                if (hasSubsplits)
                {
                    switch( MessageBox.Show(State.Form.TopLevelControl,
                        "Warning: one or more of the files specified uses subsplits.\n" +
                        "This may cause unexpected behavior.",
                        "Livesplit",
                        MessageBoxButtons.AbortRetryIgnore,
                        MessageBoxIcon.Error))
                    {
                        case DialogResult.Abort:
                            Timer.Reset();
                            break;
                        case DialogResult.Retry:
                            Timer.Reset();
                            Timer.Start();
                            break;
                        case DialogResult.Ignore:
                            Timer.UndoAllPauses();
                            break;
                    }
                }
                else
                {
                    SetButtons(false);
                    Timer.UndoAllPauses();
                }
            }
        }

        private void State_OnReset(object sender, TimerPhase value)
        {
            if (DoReset)
            {
                if (Index > 0)
                {
                    SaveRuns();
                    LoadSplits(0);
                    PendingRuns.Clear();
                    Index = 0;
                }

                SetButtons(true);
            }
        }

        private void SetButtons(bool v)
        {
            foreach (Control c in Settings.Clickables)
            {
                c.Enabled = v;
            }
        }

        public void SaveRuns()
        {
            if(PendingRuns.Count > 0)
            {
                var owner = (IWin32Window)State.Form.TopLevelControl;
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

        private void TimerUpdate() => Timer.UpdateTimes();

        private void State_OnSplit(object sender, EventArgs e)
        {
            if (State.CurrentPhase == TimerPhase.Ended && Settings.On)
            {
                Index++;

                if (LoadSplits(Index,true))
                {
                    DoReset = false;
                    Timer.Reset();
                    DoReset = true;

                    DoStart = false;
                    Timer.Start();
                    DoStart = true;
                    if (!Settings.Autostart)
                    {
                        Timer.Pause();
                    }
                }

                else
                {
                    TimerUpdate();
                    PendingRuns.Add(State.Run);
                    State.Run = FinalRun();
                }
            }
        }

        private IRun FinalRun()
        {
            Run run = new Run(new StandardComparisonGeneratorsFactory())
            {
                GameName = Settings.Game,
                CategoryName = Settings.Category
            };

            var timeOffset = new Time(new TimeSpan(), new TimeSpan());
            foreach (IRun prun in PendingRuns)
            {
                foreach (ISegment iseg in prun)
                {
                    run.AddSegment(
                        "-" + iseg.Name,
                        iseg.PersonalBestSplitTime + timeOffset,
                        iseg.BestSegmentTime + timeOffset,
                        iseg.Icon,
                        iseg.SplitTime + timeOffset
                        );
                }
                run.AddSegment(prun.GameName, default, default, null, prun.Last().SplitTime + timeOffset);
                timeOffset += prun.Last().SplitTime;
            }
            return run;
        }

        internal bool LoadSplits(int i, bool saveRun = false)
        {
            try
            {
                if (string.IsNullOrEmpty(Settings[i]))
                {
                    var run = new Run(new StandardComparisonGeneratorsFactory())
                    {
                        GameName = "",
                        CategoryName = ""
                    };
                    run.AddSegment("");

                    if (saveRun)
                    {
                        TimerUpdate();
                        PendingRuns.Add(State.Run);
                    }
                    State.Run = run;
                }
                else
                {
                    IRun run;
                    var runfact = new StandardFormatsRunFactory(Settings.Open(i), Settings[i]);
                    run = runfact.Create(new StandardComparisonGeneratorsFactory());

                    if (saveRun)
                    {
                        TimerUpdate();
                        PendingRuns.Add(State.Run);
                    }

                    State.Run = run;
                    State.Run.AutoSplitter = AutoSplitterFactory.Instance.Create(State.Run.GameName);
                }
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }
            
        public override string ComponentName => "Multiruns";

        public override void Dispose()
        {
            Settings.Dispose();
            State.OnSplit -= State_OnSplit;
            State.OnReset -= State_OnReset;
            State.OnStart -= State_OnStart;
        }

        public override XmlNode GetSettings(XmlDocument document)
        {
            XmlElement elem = document.CreateElement("Settings");
            elem.AppendChild(SettingsHelper.ToElement(document, "Version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3)));
            elem.AppendChild(SettingsHelper.ToElement(document,"Enabled",Settings.On));
            elem.AppendChild(SettingsHelper.ToElement(document, "Autostart", Settings.Autostart));
            elem.AppendChild(SettingsHelper.ToElement(document, "Game", Settings.Game));
            elem.AppendChild(SettingsHelper.ToElement(document, "Category", Settings.Category));
            var splitsElem = elem.AppendChild(document.CreateElement("Splits"));

            for (int i = 0; i < Settings.Count; i++)
            {
                var splitElem = (XmlElement)splitsElem.AppendChild(document.CreateElement("File"));
                if (!string.IsNullOrEmpty(Settings[i])){
                    splitElem.InnerText = Settings[i];
                }
            }

            return elem;
        }

        public override Control GetSettingsControl(LayoutMode mode) => Settings;

        public override void SetSettings(XmlNode settings)
        {
            var elem = (XmlElement) settings;
            Settings.On = SettingsHelper.ParseBool(elem["Enabled"], false);
            Settings.Autostart = SettingsHelper.ParseBool(elem["Autostart"], true);
            Settings.Game = SettingsHelper.ParseString(elem["Game"], string.Empty);
            Settings.Category = SettingsHelper.ParseString(elem["Category"], string.Empty);
            var splitsElem = elem["Splits"];

            if (splitsElem != null)
            {
                for (int i = 0; i < splitsElem.ChildNodes.Count; i++)
                {
                    string str = SettingsHelper.ParseString((XmlElement)splitsElem.ChildNodes.Item(i), string.Empty);
                    try
                    {
                        Settings[i] = str;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Settings.Add(str);
                    }
                }
            }
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            State = state;
        }
    }
}