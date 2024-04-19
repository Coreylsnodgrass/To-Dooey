using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace To_Dooey_Interface.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<TaskListViewModel> lists = new();


        [ObservableProperty]
        private TaskListViewModel selectedList;

        public ObservableCollection<TaskItemViewModel> SelectedListTasks => SelectedList?.Tasks;

        public MainViewModel()
        {
            // Assuming you have default lists to load, do so here
            Lists.Add(new TaskListViewModel { Name = "My Day" });
            // ... Add other lists
            SelectedList = Lists.FirstOrDefault();
        }

        [RelayCommand]
        private void AddList()
        {
            // Implementation for adding a new list
            var newList = new TaskListViewModel { Name = "New List" };
            Lists.Add(newList);
            SelectedList = newList;
        }

        [RelayCommand]
        private void AddTask()
        {
            // Implementation for adding a new task
            if (SelectedList != null)
            {
                var newTask = new TaskItemViewModel { Description = "New Task", IsCompleted = false };
                SelectedList.Tasks.Add(newTask);
            }
        }
    }

    public partial class TaskListViewModel : ObservableObject
    {
        [ObservableProperty]

        private string name;



        public ObservableCollection<TaskItemViewModel> Tasks { get; } = new();
    }

   
}
