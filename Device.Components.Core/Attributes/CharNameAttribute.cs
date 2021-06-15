using System;

namespace Device.Components.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CharNameAttribute:Attribute
    {
        public CharNameAttribute(string name)
        {
            Name = name;
        }

        public CharNameAttribute()
        {

        }

        public string Name { get; }
    }
}
