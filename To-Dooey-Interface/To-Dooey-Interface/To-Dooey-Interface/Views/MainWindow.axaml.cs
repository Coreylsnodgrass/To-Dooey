using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
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

            // Attach an event handler to the Loaded event of the window
            this.AttachedToVisualTree += MainWindow_AttachedToVisualTree;
        }

        private async void MainWindow_AttachedToVisualTree(object sender, VisualTreeAttachmentEventArgs e)
        {
            // Get a reference to the ListBox control
            var listBox = this.FindControl<ListBox>("listsListBox");

            if (listBox != null)
            {
                // Attach an event handler to the SelectionChanged event of the ListBox
                listBox.SelectionChanged += async (sender, args) =>
                {
                    if (listBox.SelectedItem != null)
                    {
                        // Call the OnListSelectedAsync method of the MainViewModel
                        await ((MainViewModel)DataContext).LoadTasksForSelectedListAsync(); ;
                    }
                };
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void Button_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
        }
    }
}
