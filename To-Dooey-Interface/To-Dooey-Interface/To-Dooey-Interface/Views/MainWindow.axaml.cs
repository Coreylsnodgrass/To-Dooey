using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using To_Dooey_Interface.ViewModels;

namespace To_Dooey_Interface.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel(); // Binding ViewModel to the View
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
