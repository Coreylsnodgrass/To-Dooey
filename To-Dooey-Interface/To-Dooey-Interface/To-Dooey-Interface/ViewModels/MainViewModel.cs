using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using To_Dooey_Interface.Services;

namespace To_Dooey_Interface.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {


        private readonly ApiService _apiService;
        private readonly IDialogService _dialogService;


        public MainViewModel()
        {
            // Initialize with default or null services, not recommended.
        }


        // Backing field for SelectedList
        [ObservableProperty]
        private ToDoListViewModel _selectedList;

        private ObservableCollection<ToDoListViewModel> _lists = new ObservableCollection<ToDoListViewModel>();

        // Commands
        public ICommand AddListCommand { get; }
        public ICommand AddTaskCommand { get; }

        // Lists property
        public ObservableCollection<ToDoListViewModel> Lists
        {
            get => _lists;
            set => SetProperty(ref _lists, value);
        }

        public MainViewModel(ApiService apiService, IDialogService dialogService)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            AddListCommand = new AsyncRelayCommand(AddListAsync);
            AddTaskCommand = new AsyncRelayCommand(AddTaskAsync);

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

        private async Task AddTaskAsync()
        {
            if (SelectedList == null)
            {
                // Optionally handle displaying a message to the user
                return;
            }

            var taskDetails = await _dialogService.ShowAddTaskDialogAsync();
            if (taskDetails.Description != null)
            {
                await _apiService.AddTaskToList(SelectedList.Id, taskDetails.Description, taskDetails.Status, taskDetails.Responsibility);
                await LoadTasksForSelectedList();
            }
        }

        private async Task LoadTasksForSelectedList()
        {
            if (SelectedList != null)
            {
                var tasks = await _apiService.GetTasksForList(SelectedList.Id);
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

        private bool ConvertStatusToIsCompleted(CompletionStatus status)
        {
            return status == CompletionStatus.Completed;
        }
    }
}
