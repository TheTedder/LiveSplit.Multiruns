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
                    Settings[i] = SettingsHelper.ParseString((XmlElement)splitsElem.ChildNodes.Item(i), string.Empty) ?? string.Empty;
                }
            }
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {

        }
    }
}
