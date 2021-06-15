using System;
using System.Collections.Generic;
using System.IO;
using Melnik.Common.Json;
using Orion.Client.Models;

namespace Orion.Client.Settings
{
    [Serializable]
    public class AppConfig : JsonFile<AppConfig>
    {
        private static AppConfig _main;
        public static AppConfig Main
        {
            get
            {
                if (_main != null) return _main;
                var configFilePath = AppSettings.AppConfigPath;

                if (File.Exists(configFilePath))
                {
                    _main = LoadFromFile(configFilePath);
                    return _main;
                }

                throw new Exception();
            }
        }

        public List<ComponentMenuItem> MenuItems { get; set; } = new List<ComponentMenuItem>();

        public static void Update()
        {
            _main = LoadFromFile(AppSettings.AppConfigPath);
        }

        protected override AppConfig GetDefaultInternal()
        {
            return new AppConfig()
            {
                MenuItems = new List<ComponentMenuItem>()
                {
                    new ComponentMenuItem()
                    {
                        ImagePath = "VALUE",
                        Name = "VALUE",
                        SearchQuery = "VALUE",
                        SearchScope = "VALUE",
                    }
                }
            };
        }
    }
}
