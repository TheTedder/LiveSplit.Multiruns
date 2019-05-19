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

namespace LiveSplit.Multiruns
{
    class MultirunsComponent : LogicComponent
    {
        public MultirunsSettings Settings;
        private readonly LiveSplitState State;
        private readonly string FirstGame;
        private string OriginalGame;

        public MultirunsComponent(LiveSplitState s)
        {
            State = s;
            Settings = new MultirunsSettings();
            OriginalGame = State.Run.GameName;
            State.OnSplit += Change;
            State.OnReset += Reset;
            try
            {
                FirstGame = (from ISegment split in State.Run where !split.Name.Substring(0, 1).Equals("-") select split.Name).First();
                State.Run.GameName = FirstGame;
            }
            catch (InvalidOperationException yourMom)
            {
                Debug.WriteLine(yourMom.Message);
            }
        }

        private void Reset(object sender, TimerPhase value)
        {
            if (Settings.On && !string.IsNullOrEmpty(FirstGame))
            {
                State.Run.GameName = FirstGame;
            }
        }

        private void Change(object sender, EventArgs e)
        {
            if (Settings.On)
            {
                //don't do anything if this is a subsplit
                if (!State.CurrentSplit.Name.Substring(0, 1).Equals("-"))
                {
                    State.Run.GameName = State.CurrentSplit.Name;
                }

                if(State.CurrentPhase == TimerPhase.Ended)
                {
                    State.Run.GameName = OriginalGame;
                }
            }
        }

        public override string ComponentName => "Multiruns";

        public override void Dispose()
        {
            State.OnSplit -= Change;
            State.OnReset -= Reset;
        }

        public override XmlNode GetSettings(XmlDocument document)
        {
            XmlElement node = document.CreateElement("Settings");
            node.AppendChild(SettingsHelper.ToElement(document, "Version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3)));
            node.AppendChild(SettingsHelper.ToElement(document,"Enabled",Settings.On));
            node.AppendChild(SettingsHelper.ToElement(document, "Game", OriginalGame));
            return node;
        }

        public override System.Windows.Forms.Control GetSettingsControl(LayoutMode mode)
        {
            return Settings;
        }

        public override void SetSettings(XmlNode settings)
        {
            Settings.On = SettingsHelper.ParseBool(((XmlElement) settings)["Enabled"],true);
            string game = SettingsHelper.ParseString(((XmlElement)settings)["Game"]);
            if (!string.IsNullOrEmpty(game))
            {
                OriginalGame = game;
            }
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {

        }
    }
}
