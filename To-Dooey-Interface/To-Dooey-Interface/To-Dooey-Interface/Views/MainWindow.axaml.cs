using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace To_Dooey_Interface.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
