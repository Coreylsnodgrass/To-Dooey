using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using To_Dooey_Interface.Services;
using To_Dooey_Interface.ViewModels;
using To_Dooey_Interface.Views;

namespace To_Dooey_Interface;

public partial class App : Application
{

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var apiService = new ApiService(); // Ensure ApiService is correctly implemented and accessible
            var dialogService = new DialogService(); // Ensure DialogService is correctly implemented and accessible

            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(apiService, dialogService)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

}
