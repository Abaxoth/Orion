namespace Device.Components.Core.Readers.Helpers
{
    internal class ReaderHelper
    {
        public static string NormalizeCpuName(string cpuName)
        {
            return cpuName.Replace("(TM)", "™")
                .Replace("(tm)", "™")
                .Replace("(R)", "®")
                .Replace("(r)", "®")
                .Replace("(C)", "©")
                .Replace("(c)", "©")
                .Replace("    ", " ")
                .Replace("  ", " ");
        }
    }
}
