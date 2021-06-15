using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using Device.Components.Core.Enums;
using Device.Components.Core.Models.Common;
using Newtonsoft.Json.Linq;
using Orion.Client.Controls;
using Orion.Client.Controls.ViewModels;
using Orion.Client.Helpers;
using Orion.Client.Settings;
using Orion.Client.ViewModels.Common;
using Orion.Client.Views;

namespace Orion.Client.ViewModels
{
    public class ComponentDetailsViewModel : BaseViewModel<ComponentDetailsViewModel, ComponentDetailsView>
    {

        public ComponentDetailsViewModel()
        {
            SetDefaultValues();
        }

        public override async Task OnLoadAsync()
        {
            var vmCollection = new List<SwitchButtonControlViewModel>();
            for (var i = 0; i < ComponentItemsInfo.Count; i++)
            {
                var save = i;
                var vm = new SwitchButtonControlViewModel
                {
                    ButtonContent = $"{i + 1}",
                    IsEnable = i > 0,
                };
                vm.OnClick += () => { SwitchInfo(save); };
                vmCollection.Add(vm);
            }

            foreach (var switchButtonControlViewModel in vmCollection)
                switchButtonControlViewModel.OtherButtons = vmCollection;


            var buttons = vmCollection.Select(ViewHelper.
                InitializeControl<SwitchButtonControl, SwitchButtonControlViewModel>).ToList();

            foreach (var button in buttons)
                SwitchButtons.Add(button);

            if (SwitchButtons.Count == 1)
                SwitchButtons.Clear();

            ComponentCharacteristic2Value = ComponentItemsInfo.First();
        }

        private void SwitchInfo(int index)
        {
            ComponentCharacteristic2Value = ComponentItemsInfo[index];
        }


        public string ImageUri
        {
            get => GetPropValue<string>(nameof(ImageUri));
            set => SetPropValue(nameof(ImageUri), value);
        }

        public string ComponentName
        {
            get => GetPropValue<string>(nameof(ComponentName));
            set => SetPropValue(nameof(ComponentName), value);
        }

        public bool IsMultiple => ComponentItemsInfo.Count > 1;

        public List<Dictionary<string, string>> ComponentItemsInfo = new List<Dictionary<string, string>>();

        public Dictionary<string, string> ComponentCharacteristic2Value
        {
            get => GetPropValue<Dictionary<string, string>>(nameof(ComponentCharacteristic2Value));
            set => SetPropValue(nameof(ComponentCharacteristic2Value), value);
        }

        public ObservableCollection<SwitchButtonControl> SwitchButtons { get; private set; }
            = new ObservableCollection<SwitchButtonControl>();

        public ICommand CloseCommand => new DelegateCommand(CloseInternal);

        public ICommand ExportCommand => new DelegateCommand(() =>
        {
            var model = new BaseComponentModel()
            {
                ComponentImagePath = ImageUri,
                ComponentName = ComponentName,
                ComponentType = ComponentType.Undefined,
                Key2Value = ComponentItemsInfo.First().ToDictionary(x => x.Key, x => x.Value),
            };

            var jsonText = JObject.FromObject(model).ToString();
            var fileName = $"[{ComponentName}] {DateTime.Now:dd-MM-yyyy hh-mm-ss}.txt";
            var filePath = Path.Combine(AppSettings.ExportDirectory, fileName);
            File.WriteAllText(filePath, jsonText);

            var answer = MessageBox.Show($"Export completed successfully{Environment.NewLine}Open export directory?",
                 "Information", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (answer == MessageBoxResult.Yes)
                Process.Start(AppSettings.ExportDirectory);
        });

        private void SetDefaultValues()
        {
            ImageUri = @"C:\Users\Supra\Desktop\processor.png";
            ComponentName = "CPU";
        }
    }
}
