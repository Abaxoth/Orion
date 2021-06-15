using System;
using System.Threading.Tasks;

namespace Orion.Client.ViewModels.Events
{
    public interface ICloseView
    {
        Task OnCloseAsync();

        event Action CloseEvent;
    }
}
