using System.Collections.Generic;
using Device.Components.Core.Asbtractions;
using Device.Components.Core.Enums;

namespace Device.Components.Core.Models.Common
{
    public class BaseComponentModel: IComponentModel
    {
        public ComponentType ComponentType { get; set; }
        public string ComponentName { get; set; }
        public string ComponentImagePath { get; set; }

        public Dictionary<string, string> Key2Value { get; set; }
            = new Dictionary<string, string>();
    }
}
