using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using To_Dooey_Interface.ViewModels;
using To_Dooey_Interface.Services;

namespace To_Dooey_Interface.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();

            // Instantiate ApiService
            var apiService = new ApiService();

            // Instantiate DialogService
            var dialogService = new DialogService();

            // Pass ApiService and DialogService to MainViewModel constructor
            DataContext = new MainViewModel(apiService, dialogService);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
