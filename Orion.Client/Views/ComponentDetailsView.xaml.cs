using System.Windows;
using Orion.Client.Helpers;

namespace Orion.Client.Views
{
    /// <summary>
    /// Interaction logic for ComponentDetailsView.xaml
    /// </summary>
    public partial class ComponentDetailsView : Window
    {
        public ComponentDetailsView()
        {
            InitializeComponent();
            SourceInitialized += ComponentDetailsView_SourceInitialized;
        }

        private void ComponentDetailsView_SourceInitialized(object sender, System.EventArgs e)
        {
            ViewEvents.RegisterView(this);
        }
    }
}
