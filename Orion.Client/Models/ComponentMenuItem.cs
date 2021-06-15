using System;

namespace Orion.Client.Models
{
    [Serializable]
    public class ComponentMenuItem
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public string SearchScope { get; set; }
        public string SearchQuery { get; set; }
    }
}
