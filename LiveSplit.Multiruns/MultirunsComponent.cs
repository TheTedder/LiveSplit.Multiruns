﻿using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LiveSplit.Multiruns
{
    class MultirunsComponent : LogicComponent
    {
        public MRSettings Settings;
        private LiveSplitState state;

        public MultirunsComponent(LiveSplitState state)
        {
            Settings = new MRSettings();
        }

        public override string ComponentName => "Multiruns";

        public override void Dispose()
        {

        }

        public override XmlNode GetSettings(XmlDocument document)
        {
            XmlElement node = document.CreateElement("Settings");
            node.AppendChild(SettingsHelper.ToElement(document, "Version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3)));
            node.AppendChild(SettingsHelper.ToElement(document,"enabled",Settings.enabled));
            return node;
        }

        public override System.Windows.Forms.Control GetSettingsControl(LayoutMode mode)
        {
            return Settings;
        }

        public override void SetSettings(XmlNode settings)
        {
            Settings.enabled = SettingsHelper.ParseBool(((XmlElement)settings)["enabled"],true);
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            
        }
    }
}
