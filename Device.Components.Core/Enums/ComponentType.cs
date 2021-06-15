using System.ComponentModel;

namespace Device.Components.Core.Enums
{
    public enum ComponentType
    {
        [Description("CPU")] Cpu,
        [Description("GPU")] Gpu,
        [Description("RAM")] Ram,
        [Description("Disk")] Disk,
        [Description("Motherboard")] Motherboard,
        [Description("Power Supply")] PowerSupply,
        Undefined,
    }
}
