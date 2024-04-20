using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using To_Dooey_Interface.ViewModels;

public class ToDoListViewModel : ObservableObject
{
    private string _name;
    private ObservableCollection<TaskItemViewModel> _tasks = new ObservableCollection<TaskItemViewModel>();

    public int Id { get; set; }

    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }
    public ToDoListViewModel(int id, string name)
    {
        Id = id;
        Name = name;
        Tasks = new ObservableCollection<TaskItemViewModel>();
    }
    public ObservableCollection<TaskItemViewModel> Tasks
    {
        get => _tasks;
        private set  // Added private setter
        {
            _tasks = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
