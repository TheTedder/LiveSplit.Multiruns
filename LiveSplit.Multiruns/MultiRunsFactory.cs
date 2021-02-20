using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;
using System.Reflection;

namespace LiveSplit.Multiruns
{
    class MultirunsFactory : IComponentFactory
    {
        public string ComponentName => "Multiruns";

        public string Description => "allows for marathon-type multiruns with proper load timing and autosplitting";

        public ComponentCategory Category => ComponentCategory.Control;

        public string UpdateName => ComponentName;

        public string XMLURL => "";

        public string UpdateURL => "";

        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        public IComponent Create(LiveSplitState state)
        {
            return new MultirunsComponent(state);
        }
    }
}