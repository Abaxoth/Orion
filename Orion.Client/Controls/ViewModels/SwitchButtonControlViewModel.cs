using System;
using System.Collections.Generic;
using System.Windows.Input;
using DevExpress.Mvvm;
using Orion.Client.ViewModels.Common;

namespace Orion.Client.Controls.ViewModels
{
    public class SwitchButtonControlViewModel: BaseViewModel<SwitchButtonControlViewModel, SwitchButtonControl>
    {
        public string ButtonContent
        {
            get => GetPropValue<string>(nameof(ButtonContent));
            set => SetPropValue(nameof(ButtonContent), value);
        }

        public event Action OnClick;

        public List<SwitchButtonControlViewModel> OtherButtons { get; set; } = new List<SwitchButtonControlViewModel>();

        public bool IsEnable
        {
            get => GetPropValue<bool>(nameof(IsEnable));
            set => SetPropValue(nameof(IsEnable), value);
        }

        public ICommand ClickCommand => new DelegateCommand(() =>
        {
            foreach (var switchButtonControlViewModel in OtherButtons)
                switchButtonControlViewModel.IsEnable = true;

            IsEnable = false;
            OnClick?.Invoke();
        });
    }
}
