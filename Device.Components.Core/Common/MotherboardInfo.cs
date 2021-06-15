using System;
using System.Management;
using System.Text;

namespace Device.Components.Core.Common
{
    internal static class MotherboardInfo
    {
        private static ManagementObjectSearcher baseboardSearcher =
            new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");

        private static ManagementObjectSearcher motherboardSearcher =
            new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_MotherboardDevice");

        private static ManagementObject Motherboard
        {
            get
            {
                var collection = motherboardSearcher.Get();

                foreach (var a in collection)
                {
                    return (ManagementObject) a;
                }

                throw new EncoderFallbackException();
            }
        }

        public static string Availability
        {
            get
            {
                try
                {
                    return GetAvailability(int.Parse($"{Motherboard["Availability"]}"));
                   
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public static bool HostingBoard
        {
            get
            {
                try
                {
                    return Motherboard["HostingBoard"].ToString() == "True";
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public static string InstallDate
        {
            get
            {
                try
                {
                    var installDate = Motherboard["InstallDate"];
                    return string.IsNullOrWhiteSpace((string)installDate) 
                        ? "" 
                        : ConvertToDateTime(Motherboard["InstallDate"].ToString());
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }

        public static string Manufacturer
        {
            get
            {
                try
                {
                    return Motherboard["Manufacturer"].ToString();
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }

        public static string Model
        {
            get
            {
                try
                {
                    return Motherboard["Model"].ToString();
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }

        public static string PartNumber
        {
            get
            {
                try
                {
                    return Motherboard["PartNumber"].ToString();
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }

        public static string PNPDeviceID
        {
            get
            {
                try
                {
                    return Motherboard["PNPDeviceID"].ToString();
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }

        public static string PrimaryBusType
        {
            get
            {
                try
                {
                    return Motherboard["PrimaryBusType"].ToString();
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }

        public static string Product
        {
            get
            {
                try
                {
                    return Motherboard["Product"].ToString();
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }

        public static bool Removable
        {
            get
            {
                try
                {
                    return Motherboard["Removable"].ToString() == "True";
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public static bool Replaceable
        {
            get
            {
                try
                {
                    return Motherboard["Replaceable"].ToString() == "True";
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public static string RevisionNumber
        {
            get
            {
                try
                {
                    return Motherboard["RevisionNumber"].ToString();
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }

        public static string SecondaryBusType
        {
            get
            {
                try
                {
                    return Motherboard["SecondaryBusType"].ToString();
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }

        public static string SerialNumber
        {
            get
            {
                try
                {
                    return Motherboard["SerialNumber"].ToString();
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }

        public static string Status
        {
            get
            {
                try
                {
                    return Motherboard["Status"].ToString();
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }

        public static string SystemName
        {
            get
            {
                try
                {
                    return Motherboard["SystemName"].ToString();
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }

        public static string Version
        {
            get
            {
                try
                {
                    return Motherboard["Version"].ToString();
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }

        private static string GetAvailability(int availability)
        {
            switch (availability)
            {
                case 1: return "Other";
                case 2: return "Unknown";
                case 3: return "Running or Full Power";
                case 4: return "Warning";
                case 5: return "In Test";
                case 6: return "Not Applicable";
                case 7: return "Power Off";
                case 8: return "Off Line";
                case 9: return "Off Duty";
                case 10: return "Degraded";
                case 11: return "Not Installed";
                case 12: return "Install Error";
                case 13: return "Power Save - Unknown";
                case 14: return "Power Save - Low Power Mode";
                case 15: return "Power Save - Standby";
                case 16: return "Power Cycle";
                case 17: return "Power Save - Warning";
                default: return "Unknown";
            }
        }

        private static string ConvertToDateTime(string unconvertedTime)
        {
            string convertedTime = "";
            int year = int.Parse(unconvertedTime.Substring(0, 4));
            int month = int.Parse(unconvertedTime.Substring(4, 2));
            int date = int.Parse(unconvertedTime.Substring(6, 2));
            int hours = int.Parse(unconvertedTime.Substring(8, 2));
            int minutes = int.Parse(unconvertedTime.Substring(10, 2));
            int seconds = int.Parse(unconvertedTime.Substring(12, 2));
            string meridian = "AM";
            if (hours > 12)
            {
                hours -= 12;
                meridian = "PM";
            }
            convertedTime = date.ToString() + "/" + month.ToString() + "/" + year.ToString() + " " +
                            hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString() + " " + meridian;
            return convertedTime;
        }
    }
}
