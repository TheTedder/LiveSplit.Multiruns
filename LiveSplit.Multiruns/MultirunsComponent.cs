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
            Settings[0] = State.Run.FilePath;
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
            if (!string.IsNullOrEmpty(Settings[i]))
            {
                var compgenfact = new StandardComparisonGeneratorsFactory();
                var runfact = new XMLRunFactory(Settings.Open(i),Settings[i]);
                var run = runfact.Create(compgenfact);
                State.Run = run;
                Timer.Reset(true);
            }
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
            node.AppendChild(SettingsHelper.ToElement(document, "Next", Settings[0]));
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
            Settings[0] = SettingsHelper.ParseString(elem["Next"],string.Empty);
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {

        }
    }
}
