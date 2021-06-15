using System;
using System.Windows;
using System.Windows.Controls;
using Orion.Client.ViewModels.Common;

namespace Orion.Client.Helpers
{
    public class ViewHelper
    {
        public static TControl InitializeControl<TControl, TControlViewModel>(TControlViewModel viewModel)
            where TControl : Control
            where TControlViewModel : BaseViewModel<TControlViewModel, TControl>
        {
            var control = (Control)Activator.CreateInstance(typeof(TControl));
            if (control == null)
                throw new Exception($"Can't initialize: {typeof(TControl)}");

            control.DataContext = viewModel;
            return (TControl)control;
        }

        public static void Show<TView, TViewModel>(TViewModel viewModel, bool asDialog = true)
            where TView : Window
            where TViewModel : BaseViewModel<TViewModel, TView>
        {
            var view = InitializeControl<TView, TViewModel>(viewModel);
            if (asDialog)
                view.ShowDialog();
            else
                view.Show();
        }
    }
}
