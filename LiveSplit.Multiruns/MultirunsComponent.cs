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

namespace LiveSplit.Multiruns
{
    public class MultirunsComponent : LogicComponent
    {
        private readonly MultirunsSettings Settings;
        private readonly LiveSplitState State;
        private readonly TimerModel Timer;

        public MultirunsComponent(LiveSplitState s)
        {
            State = s;
            Timer = new TimerModel { CurrentState = State };
            Settings = new MultirunsSettings(this);
            State.OnSplit += State_OnSplit;
            Settings.tbSplitsFile.Text = State.Run.FilePath;
        }

        private void State_OnSplit(object sender, EventArgs e)
        {
            if (State.CurrentPhase == TimerPhase.Ended && Settings.On)
            {
                LoadSplits(0);
                Timer.Start();
            }
        }

        public void LoadSplits(int i)
        {
            var runfact = new XMLRunFactory(Settings.Open(),Settings.tbSplitsFile.Text);
            var compgenfact = new StandardComparisonGeneratorsFactory();
            var run = runfact.Create(compgenfact);
            State.Run = run;
            Timer.Reset(true);
        }

        public override string ComponentName => "Multiruns";

        public override void Dispose()
        {
            State.OnSplit -= State_OnSplit;
        }
        public override XmlNode GetSettings(XmlDocument document)
        {
            XmlElement node = document.CreateElement("Settings");
            node.AppendChild(SettingsHelper.ToElement(document, "Version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3)));
            node.AppendChild(SettingsHelper.ToElement(document,"Enabled",Settings.On));
            node.AppendChild(SettingsHelper.ToElement(document, "Next", Settings.tbSplitsFile.Text));
            return node;
        }

        public override System.Windows.Forms.Control GetSettingsControl(LayoutMode mode)
        {
            return Settings;
        }

        public override void SetSettings(XmlNode settings)
        {
            Debug.WriteLine("Loaded settings node " + settings.InnerText);
            var elem = (XmlElement) settings;
            Settings.On = SettingsHelper.ParseBool(elem["Enabled"], false);
            Settings.tbSplitsFile.Text = SettingsHelper.ParseString(elem["Next"],string.Empty);

            if (string.IsNullOrEmpty(Settings.tbSplitsFile.Text))
            {
                State.Run = new Run(new StandardComparisonGeneratorsFactory());
            }
            else
            {
                LoadSplits(0);
            }
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {

        }
    }
}
