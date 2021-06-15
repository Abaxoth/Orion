using System;
using System.Windows;
using Orion.Client.Exceptions;

namespace Orion.Client.Helpers
{
    public static class AppHelper
    {
        public static void ShowInfo(string message)
        {
            MessageBox.Show(message, "Information",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void ShowException(Exception ex)
        {
            if (ex is WarningException invalidEx)
            {
                ShowWarning(invalidEx);
            }
            else
            {
                MessageBox.Show(ex.ToString(), ex.Message,
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static void ShowWarning(WarningException ex)
        {
            var tittle = BoxImage2Name(ex.BoxImage);
            MessageBox.Show(ex.Message, tittle, MessageBoxButton.OK, ex.BoxImage);
        }

        private static string BoxImage2Name(MessageBoxImage image)
        {
            switch (image)
            {
                case MessageBoxImage.Error:
                    return "Error";
                case MessageBoxImage.Warning:
                    return "Warning";
                case MessageBoxImage.Information:
                    return "Information";
                default:
                    return "Message";
            }
        }
    }
}
