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
                State.Run.GameName = FindNextSplit();
            }
            catch (InvalidOperationException yourMom)
            {
                Debug.WriteLine(yourMom.Message);
            }
        }

        public string FindNextSplit()
        {
            ISegment[] splits;

            switch (State.CurrentPhase)
            {
                case TimerPhase.Ended:
                case TimerPhase.NotRunning:
                    splits = State.Run.ToArray();
                    break;

                case TimerPhase.Paused:
                case TimerPhase.Running:
                    splits = new ISegment[State.Run.Count() - State.CurrentSplitIndex];
                    Array.Copy(State.Run.ToArray(), State.CurrentSplitIndex, splits, 0, splits.Length);
                    break;

                default:
                    splits = Array.Empty<ISegment>();
                    break;
            }

            return (from ISegment split in splits where !split.Name.Substring(0, 1).Equals("-") select split.Name).First();
        }

        private void Reset(object sender, TimerPhase value)
        {
            if (Settings.On && !string.IsNullOrEmpty(FindNextSplit()))
            {
                State.Run.GameName = FindNextSplit();
            }
        }

        private void Change(object sender, EventArgs e)
        {
            if (Settings.On)
            {
                if(State.CurrentPhase == TimerPhase.Ended)
                {
                    State.Run.GameName = OriginalGame;
                }
                else
                {
                    string game = FindNextSplit();
                    Debug.WriteLine("Found " + game);
                    if (!game.Equals(State.Run.GameName))
                    {
                        State.Run.GameName = game;
                    }
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
