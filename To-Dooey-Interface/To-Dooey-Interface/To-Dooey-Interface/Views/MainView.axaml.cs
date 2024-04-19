using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using To_Dooey_Interface.ViewModels;

namespace To_Dooey_Interface.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
