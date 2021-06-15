using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Benchmark.Models;
using Benchmark.Tools;
using DevExpress.Mvvm;
using Device.Components.Core.Models.Common;
using Device.Components.Core.Readers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Orion.Client.Controls;
using Orion.Client.Helpers;
using Orion.Client.Settings;
using Orion.Client.ViewModels.Common;
using Orion.Client.Views;

namespace Orion.Client.ViewModels
{
    public class MainViewModel : BaseViewModel<MainViewModel, MainView>
    {
        public MainViewModel()
        {
            AddDeviceComponents();
            AppConfig.GetDefault(Path.Combine(AppSettings.DataDirectory, "DefaultConfig.json")).Save();
        }

        public ObservableCollection<ComponentControl> ComputerComponents { get; set; } =
            new ObservableCollection<ComponentControl>();

        public ObservableCollection<ComponentControl> VirtualComputerComponents { get; set; } =
            new ObservableCollection<ComponentControl>();

        public string Tittle => $"Orion {AppSettings.Version}";

        public long SingleCpuScore { get; set; }
        public long MultipleCpuScore { get; set; }

        public long Value { get; set; }

        public ICommand ExportBenchmarkResult => new DelegateCommand(() =>
        {
            var result = new CpuBenchmarkResult()
            {
                SingleCoreScore = SingleCpuScore,
                MultipleCoreScore = MultipleCpuScore,
            };

            ExportObject("CPU Benchmark result", result);
        });

        public ICommand LoadVirtualPc => new DelegateCommand(() =>
        {
            VirtualComputerComponents.Clear();
            var files = Directory.GetFiles(Path.Combine(AppSettings.DataDirectory, "Virtual"));
            var components = new List<BaseComponentModel>();

            foreach (var file in files)
            {
                var json = File.ReadAllText(file);
                var component = JsonConvert.DeserializeObject<BaseComponentModel>(json);
                components.Add(component);
            }

            AddVirtualComponent(components.ToArray());
        });

        public ICommand ExecuteCpuBenchmark => new DelegateCommand(() =>
        {
            Value = 0;
            OnPropertyChanged(nameof(Value));
            var driver = new BenchmarkDriverFactory().CreateCpuDriver();
            var result = driver.Execute();
            SingleCpuScore = result.SingleCoreScore;
            MultipleCpuScore = result.MultipleCoreScore;
            Value = 100;

            OnPropertyChanged(nameof(SingleCpuScore));
            OnPropertyChanged(nameof(MultipleCpuScore));
            OnPropertyChanged(nameof(Value));
        });

        public ICommand UpdateComponents => new DelegateCommand(() =>
        {
            try
            {
                ComputerComponents.Clear();
                AddDeviceComponents();
            }
            catch (Exception ex)
            {
                AppHelper.ShowException(ex);
            }
        });

        private void AddDeviceComponents()
        {
            var reader = new ComponentsReader();
            AppendMenuItems();
            AddComponent(reader.ReadHddCollection().ToArray());
        }

        private void AppendMenuItems()
        {
            var reader = new ComponentsReader();
            AppConfig.Update();


            foreach (var menuItem in AppConfig.Main.MenuItems)
            {
                var items = reader.
                    ReadComponents<BaseComponentModel>(menuItem.SearchScope, menuItem.SearchQuery)
                    .ToArray();

                foreach (var baseComponentModel in items)
                {
                    baseComponentModel.ComponentName = menuItem.Name;
                    baseComponentModel.ComponentImagePath = menuItem.ImagePath;
                }

                AddComponent(items.ToArray());
            }
        }

        private void ExportObject(string name, object obj)
        {
            var jsonText = JObject.FromObject(obj).ToString();
            var fileName = $"[{name}] {DateTime.Now:dd-MM-yyyy hh-mm-ss}.txt";
            var filePath = Path.Combine(AppSettings.ExportDirectory, fileName);
            File.WriteAllText(filePath, jsonText);

            var answer = MessageBox.Show($"Export completed successfully{Environment.NewLine}Open export directory?",
                "Information", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (answer == MessageBoxResult.Yes)
                Process.Start(AppSettings.ExportDirectory);
        }

        private void AddComponent<TComponentModel>(params TComponentModel[] models)
            where TComponentModel : BaseComponentModel
        {
            if (!models.Any()) return;

            var control = ComponentsHelper.InitializeUniversalComponentControl(false, models);
            ComputerComponents.Add(control);
        }

        private void AddVirtualComponent<TComponentModel>(params TComponentModel[] models)
            where TComponentModel : BaseComponentModel
        {
            if (!models.Any()) return;

            var control = ComponentsHelper.InitializeUniversalComponentControl(true, models);
            VirtualComputerComponents.Add(control);
        }

    }
}
