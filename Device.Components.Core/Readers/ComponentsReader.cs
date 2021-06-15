using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Reflection;
using Device.Components.Core.Attributes;
using Device.Components.Core.Enums;
using Device.Components.Core.Models;
using Device.Components.Core.Models.Common;

namespace Device.Components.Core.Readers
{
    public class ComponentsReader
    {
        private readonly Dictionary<ComponentType, ManagementObjectSearcher> _compType2Searcher = new()
        {
            { ComponentType.Ram, new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory") },
            { ComponentType.Cpu, new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM  Win32_Processor") },
            { ComponentType.Gpu, new ManagementObjectSearcher("select * from Win32_VideoController") },
            { ComponentType.Motherboard, new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_MotherboardDevice") },
            { ComponentType.PowerSupply, new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VoltageProbe") },
        };

        public IEnumerable<TModel> ReadComponentCollection<TModel>(ComponentType type)
            where TModel : BaseComponentModel, new()
        {
            var searcher = _compType2Searcher[type];
            return ReadComponents<TModel>(type, searcher);
        }

        public IEnumerable<HardDriveModel> ReadHddCollection()
        {
            var list = new List<HardDriveModel>();

            foreach (var driveInfo in DriveInfo.GetDrives())
            {
                var model = new HardDriveModel
                {
                    ComponentType = ComponentType.Disk,
                    AvailableFreeSpaceInGb = driveInfo.AvailableFreeSpace / 1024 / 1024 / 1024,
                    DriveFormat = driveInfo.DriveFormat,
                    IsReady = driveInfo.IsReady,
                    DriveType = driveInfo.DriveType,
                    Name = driveInfo.Name,
                    RootDirectory = driveInfo.RootDirectory.FullName,
                    TotalFreeSpaceImGb = driveInfo.TotalFreeSpace / 1024 / 1024 / 1024,
                    TotalSizeInGb = driveInfo.TotalSize / 1024 / 1024 / 1024,
                };

                list.Add(model);
            }

            return list;
        }

        public IEnumerable<TModel> ReadComponents<TModel>(string scope, string query)
            where TModel : BaseComponentModel, new()
        {
            var collection = new List<TModel>();
            var searcher = new ManagementObjectSearcher(scope, query);

            foreach (var o in searcher.Get())
            {
                var obj = (ManagementObject)o;
                var model = new TModel()
                {
                    ComponentType = ComponentType.Undefined,
                };

                foreach (var prop in obj.Properties)
                {
                    var name = prop.Name;
                    var modelProperty = GetPropertyByDepNameOrNull<TModel>(name);
                    if (modelProperty != null)
                    {
                        modelProperty.SetValue(model, $"{prop.Value}");
                        continue;
                    }

                    model.Key2Value.Add(prop.Name, $"{prop.Value}");
                }

                collection.Add(model);
            }

            return collection;
        }

        public IEnumerable<TModel> ReadComponents<TModel>(ComponentType type,
            ManagementObjectSearcher searcher)
            where TModel : BaseComponentModel, new()
        {
            var collection = new List<TModel>();

            foreach (var o in searcher.Get())
            {
                var obj = (ManagementObject)o;
                var model = new TModel()
                {
                    ComponentType = type,
                };

                foreach (var prop in obj.Properties)
                {
                    var name = prop.Name;
                    var modelProperty = GetPropertyByDepNameOrNull<TModel>(name);
                    if (modelProperty != null)
                    {
                        modelProperty.SetValue(model, $"{prop.Value}");
                        continue;
                    }

                    model.Key2Value.Add(prop.Name, $"{prop.Value}");
                }

                collection.Add(model);
            }

            return collection;
        }

        private PropertyInfo GetPropertyByDepNameOrNull<TModel>(string depName)
            where TModel : BaseComponentModel
        {
            foreach (var prop in typeof(TModel).GetProperties())
            {
                var propDepName = prop.GetCustomAttribute<DepNameAttribute>()?.Name;
                if (propDepName == prop.Name)
                    return prop;
            }

            return null;
        }
    }
}
