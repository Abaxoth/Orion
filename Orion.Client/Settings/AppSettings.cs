using System.IO;

namespace Orion.Client.Settings
{
    public static class AppSettings
    {
        static AppSettings()
        {
            EnsureDirectoryExists(DataDirectory);
            EnsureDirectoryExists(LogDirectory);
            EnsureDirectoryExists(ExportDirectory);
        }

        public static string Version => "0.1.1";
        public static string DataDirectory => "Data";
        public static string LogDirectory => Path.Combine(DataDirectory, "Logs");
        public static string ExportDirectory => Path.Combine(DataDirectory, "Export");
        public static string AppConfigPath => Path.Combine(DataDirectory, "AppConfig.json");

        public static void EnsureDirectoryExists(string directoryPath)
        {
            if (Directory.Exists(directoryPath)) return;
            Directory.CreateDirectory(directoryPath);
        }

    }
}
