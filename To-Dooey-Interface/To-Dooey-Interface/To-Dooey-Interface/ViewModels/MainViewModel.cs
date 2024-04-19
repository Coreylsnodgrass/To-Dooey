using Avalonia.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace To_Dooey_Interface.ViewModels;

// Ensure MyItemType is defined somewhere in your project
public class MyItemType
{
    // Replace these properties with the actual ones you need
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
}

public partial class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    public ObservableCollection<MyItemType> MyItems { get; }

    public MainViewModel()
    {
        MyItems = new ObservableCollection<MyItemType>();
        // Populate your collection with some sample data
        MyItems.Add(new MyItemType { Title = "Sample Task", IsCompleted = false });
        if (Design.IsDesignMode)
        {
            LoadDesignTimeData();
        }
    }
    private void LoadDesignTimeData()
    {
        // Load design time data into MyItems
        MyItems.Add(new MyItemType { /* properties */ });
    }
}
