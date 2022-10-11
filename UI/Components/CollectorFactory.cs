using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;

[assembly: ComponentFactory(typeof(CollectorFactory))]

namespace LiveSplit.UI.Components
{
    public class CollectorFactory : IComponentFactory
    {
        public string ComponentName => "therun.gg";

        public string Description => "Uploads your runs to therun.gg";

        public ComponentCategory Category => ComponentCategory.Other;

        public IComponent Create(LiveSplitState state) => new CollectorComponent(state);

        public string UpdateName => ComponentName;

        public string XMLURL => "";

        public string UpdateURL => "";

        public Version Version => Version.Parse("0.1.3");
    }
}