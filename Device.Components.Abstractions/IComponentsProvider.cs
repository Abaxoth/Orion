using Device.Components.Models;

namespace Device.Components.Service
{
    public interface IComponentsProvider
    {
        CpuMeta GetCpuMeta();
    }
}
