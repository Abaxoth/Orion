using System;

namespace Device.Components.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DepNameAttribute: Attribute
    {
        public DepNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
