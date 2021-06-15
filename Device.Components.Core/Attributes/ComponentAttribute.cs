using System;

namespace Device.Components.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute:Attribute
    {
        public ComponentAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
