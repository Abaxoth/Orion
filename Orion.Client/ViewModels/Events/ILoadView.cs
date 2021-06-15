using System.Threading.Tasks;

namespace Orion.Client.ViewModels.Events
{
    public interface ILoadView
    {
        Task OnLoadAsync();
    }
}
