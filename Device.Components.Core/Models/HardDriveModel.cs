using System.IO;
using Device.Components.Core.Attributes;
using Device.Components.Core.Models.Common;

namespace Device.Components.Core.Models
{
    [Component("HDD")]
    public class HardDriveModel : BaseComponentModel
    {
        [CharName("Name:")] public string Name { get; set; }
        [CharName("Drive type:")] public DriveType DriveType { get; set; }
        [CharName("Drive format:")] public string DriveFormat { get; set; }
        [CharName("Is ready:")] public bool IsReady { get; set; }
        [CharName("Available free space (Gb):")] public long AvailableFreeSpaceInGb { get; set; }
        [CharName("Total free space (Gb):")] public long TotalFreeSpaceImGb { get; set; }
        [CharName("Total size (Gb):")] public long TotalSizeInGb { get; set; }
        [CharName("Root directory:")] public string RootDirectory { get; set; }
    }
}
