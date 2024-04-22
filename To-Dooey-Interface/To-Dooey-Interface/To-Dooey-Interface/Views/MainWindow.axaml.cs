using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using To_Dooey_Interface.Services;
using To_Dooey_Interface.ViewModels;

namespace To_Dooey_Interface.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var apiService = new ApiService(); // Ensure ApiService is properly set up to handle requests
            var dialogService = new DialogService(); // You need to define this class or get an instance from a service provider
            DataContext = new MainViewModel(apiService, dialogService);
            

        }


        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnPointerEntered(object sender, PointerEventArgs e)
        {
            // Cast the sender to Border and change its background color
            var border = sender as Border;
            border.Background = new SolidColorBrush(Color.Parse("Gray")); // Light pink color for example
        }

        private void OnPointerExited(object sender, PointerEventArgs e)
        {
            // Cast the sender to Border and revert its background color
            var border = sender as Border;
            border.Background = new SolidColorBrush(Color.Parse("LightBlue"));
        }

    }
}
