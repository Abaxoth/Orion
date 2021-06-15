using System.Collections.Generic;
using System.Windows.Input;
using DevExpress.Mvvm;
using Orion.Client.Helpers;
using Orion.Client.ViewModels;
using Orion.Client.ViewModels.Common;
using Orion.Client.Views;

namespace Orion.Client.Controls.ViewModels
{
    public class ComponentControlViewModel : BaseViewModel<ComponentControlViewModel, ComponentControl>
    {
        public ComponentControlViewModel()
        {
            ImageUri = @"C:\Users\Supra\Desktop\processor.png";
        }

        public decimal BlurRadius { get; set; }
        public string ComponentName
        {
            get => GetPropValue<string>(nameof(ComponentName));
            set => SetPropValue(nameof(ComponentName), value);
        }

        public List<Dictionary<string, string>> ComponentItemsInfo = new List<Dictionary<string, string>>();

        public string ImageUri
        {
            get => GetPropValue<string>(nameof(ImageUri));
            set => SetPropValue(nameof(ImageUri), value);
        }

        public ICommand Details => new DelegateCommand(() =>
        {
            var viewModel = new ComponentDetailsViewModel
            {
                ImageUri = ImageUri,
                ComponentName = ComponentName,
                ComponentItemsInfo = ComponentItemsInfo,
            };

            ViewHelper.Show<ComponentDetailsView, ComponentDetailsViewModel>(viewModel);
        });
    }
}
