using System;
using System.Windows;

namespace Orion.Client.Exceptions
{
    public class WarningException : Exception
    {
        public WarningException(string message, MessageBoxImage image) : base(message)
        {
            BoxImage = image;
        }

        public WarningException(string message)
        {
            BoxImage = MessageBoxImage.Warning;
        }

        public MessageBoxImage BoxImage { get; }
    }
}
