using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Orion.Client.ViewModels.Events;

namespace Orion.Client.ViewModels.Common
{
    public class BaseViewModel<TParentViewModel, TWindow> : DependencyObject,
        INotifyPropertyChanged, ILoadView, ICloseView
        where TParentViewModel : DependencyObject
        where TWindow : Control
    {
        private readonly Dictionary<object, object> _propName2PropValue = 
            new Dictionary<object, object>();
        public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel()
        {
        }

        public event Action CloseEvent;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected T GetPropValue<T>(string propertyName)
        {
            if (!_propName2PropValue.ContainsKey(propertyName))
                _propName2PropValue.Add(propertyName, null);

            var value = _propName2PropValue[propertyName];
            if (value == null)
                return default;

            return (T)value;
        }

        protected void SetPropValue<T>(string propertyName, T value)
        {
            _propName2PropValue[propertyName] = value;
            OnPropertyChanged(propertyName);
        }

        public virtual async Task OnLoadAsync()
        {
        }

        public virtual async Task OnCloseAsync()
        {
        }

        protected void CloseInternal()
        {
            CloseEvent?.Invoke();
        }
    }
}
