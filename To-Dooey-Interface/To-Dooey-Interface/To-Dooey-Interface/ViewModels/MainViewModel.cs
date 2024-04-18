using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using To_Dooey_Interface.ViewModels;


public class MainViewModel : INotifyPropertyChanged
{
    public ObservableCollection<ToDoListViewModel> Lists { get; } = new ObservableCollection<ToDoListViewModel>();

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Add commands and methods for the UI actions
}
