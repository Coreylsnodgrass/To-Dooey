using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.OpenGL;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using To_Dooey_Interface.Services;

namespace To_Dooey_Interface.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        // Commands
        public ICommand AddListCommand { get; }
        public ICommand AddTaskCommand { get; }
        public ICommand UpdateListCommand { get; }
        public ICommand UpdateTaskCommand { get; }
        public ICommand DeleteListCommand { get; }
        public ICommand DeleteTaskCommand { get; }

        private readonly ApiService _apiService;
        private readonly IDialogService _dialogService;
        private int _currentBackgroundIndex = 0;
        public event PropertyChangedEventHandler PropertyChanged;
        private string _currentBackground;

        [ObservableProperty]
        private ToDoListViewModel selectedList;

        private ObservableCollection<ToDoListViewModel> lists = new ObservableCollection<ToDoListViewModel>();
        protected virtual void OnPropertyChanged(string propertyName) =>
       PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // Lists property
        public ObservableCollection<ToDoListViewModel> Lists
        {
            get => lists;
            set => SetProperty(ref lists, value);
        }
       
        public MainViewModel(ApiService apiService, IDialogService dialogService)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            AddListCommand = new AsyncRelayCommand(AddListAsync);
            AddTaskCommand = new AsyncRelayCommand(AddTaskAsync);
            UpdateListCommand = new AsyncRelayCommand(UpdateListAsync);
            UpdateTaskCommand = new AsyncRelayCommand(UpdateTaskAsync);
            DeleteListCommand = new AsyncRelayCommand(DeleteListAsync);
            DeleteTaskCommand = new AsyncRelayCommand(DeleteTaskAsync);


            LoadData();
        }
        private async Task LoadData()
        {
            try
            {
                var apiLists = await _apiService.GetListsAsync(); 
                Lists.Clear();
                foreach (var apiList in apiLists)
                {
                    Lists.Add(new ToDoListViewModel(apiList.Id, apiList.Name));
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error loading data: {ex.Message}");
            }
        }

        private async Task AddListAsync()
        {
            var newListName = await _dialogService.ShowAddListDialogAsync();
            if (!string.IsNullOrWhiteSpace(newListName))
            {
                await _apiService.CreateList(newListName);
                await LoadData();
            }
        }
        private async Task UpdateListAsync()
        {
            if (SelectedList == null) return;
            var newListName = await _dialogService.ShowUpdateListDialogAsync(SelectedList.Name);
            if (!string.IsNullOrWhiteSpace(newListName))
            {
                await _apiService.UpdateList(SelectedList.Id, newListName);
                await LoadData();
            }
        }
        private async Task DeleteListAsync()
        {
            if (SelectedList == null) return;
            await _apiService.DeleteList(SelectedList.Id);
            await LoadData();
        }

        private async Task AddTaskAsync()
        {
            if (SelectedList == null) return;
            var taskDetails = await _dialogService.ShowAddTaskDialogAsync();
            if (taskDetails.Description != null)
            {
                await _apiService.AddTaskToList(SelectedList.Id, taskDetails.Description, taskDetails.Status, taskDetails.Responsibility);
                await LoadTasksForSelectedListAsync();
            }
        }
        private async Task UpdateTaskAsync()
        {
            var selectedTask = SelectedList?.Tasks.FirstOrDefault(t => t.IsSelected);
            if (selectedTask == null) return;
            var taskDetails = await _dialogService.ShowUpdateTaskDialogAsync(selectedTask);
            if (taskDetails.Description != null || taskDetails.Status != default || taskDetails.Responsibility != null)
            {
                await _apiService.UpdateTask(selectedTask.Id, taskDetails.Description, taskDetails.Status, taskDetails.Responsibility);
                await LoadTasksForSelectedListAsync();
            }
        }
        private async Task DeleteTaskAsync()
        {
            var tasksToDelete = SelectedList?.Tasks.Where(t => t.IsSelected).ToList();
            if (tasksToDelete == null || !tasksToDelete.Any()) return;

            foreach (var task in tasksToDelete)
            {
                await _apiService.DeleteTask( task.Id);
            }
            await LoadTasksForSelectedListAsync(); // Reload the task list post deletion
        }

        //public ToDoListViewModel _SelectedList
        //{
        //    get => _selectedList;
        //    set
        //    {
        //        if (SetProperty(ref _selectedList, value))
        //        {
        //            // This will automatically call LoadTasksForSelectedListAsync whenever the selected list changes.
        //            LoadTasksForSelectedListAsync();
        //        }
        //    }
        //}
        public async Task LoadTasksForSelectedListAsync()
        {
            if (SelectedList != null)
            {
                var tasks = await _apiService.GetTasksForListAsync(SelectedList.Id);
                SelectedList.Tasks.Clear();
                foreach (var task in tasks)
                {
                    SelectedList.Tasks.Add(new TaskItemViewModel
                    {
                        Id = task.Id,
                        Description = task.Description,
                        Status = task.Status,
                        Responsibility = task.Responsibility,
                        IsCompleted = ConvertStatusToIsCompleted(task.Status)
                    });
                }
            }
        }

        // This method is automatically called after SelectedList is changed.
        partial void OnSelectedListChanged(ToDoListViewModel value)
        {
            // Custom logic after the selected list changes, like loading tasks.
            LoadTasksForSelectedListAsync();
        }


        private bool ConvertStatusToIsCompleted(CompletionStatus status)
        {
            return status == CompletionStatus.Completed;
        }
    }
}
