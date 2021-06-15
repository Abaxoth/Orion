using System.Windows;
using Orion.Client.Helpers;

namespace Orion.Client.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            SourceInitialized += MainView_SourceInitialized;
        }

        private void MainView_SourceInitialized(object sender, System.EventArgs e)
        {
            ViewEvents.RegisterView(this);
        }
    }
}
