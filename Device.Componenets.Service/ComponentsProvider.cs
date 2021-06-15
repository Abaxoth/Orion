using System;
using System.Collections.Generic;
using System.Text;
using Device.Components.Models;
using Device.Components.Service;

namespace Device.Componenets.Service
{
    public class ComponentsProvider: IComponentsProvider
    {
        public CpuMeta GetCpuMeta()
        {
            return new CpuMeta();
        }
    }
}
