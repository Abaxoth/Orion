using System.Windows;
using Orion.Client.ViewModels.Events;

namespace Orion.Client.Helpers
{
    public static class ViewEvents
    {
        public static void RegisterView(Window window)
        {
            if (window.DataContext is ILoadView viewModel)
            {
                window.Loaded += (_, __) => viewModel.OnLoadAsync();
            }

            if (window.DataContext is ICloseView viewModel2)
            {
                window.Closed += (_, __) => viewModel2.OnCloseAsync();
                viewModel2.CloseEvent += window.Close;
            }
        }

    }
}
