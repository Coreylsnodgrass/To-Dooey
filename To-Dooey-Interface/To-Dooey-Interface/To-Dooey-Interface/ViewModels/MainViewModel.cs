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

        //[ObservableProperty]
        //rivate ObservableCollection<ToDoListViewModel> lists;

        [ObservableProperty]
        private ToDoListViewModel selectedList;
        private ObservableCollection<ToDoListViewModel> lists = new ObservableCollection<ToDoListViewModel>();


        public ICommand AddListCommand { get; }
        public ICommand AddTaskCommand { get; }

        public MainViewModel()
        {
           
        }
        public ObservableCollection<ToDoListViewModel> Lists
        {
            get { return lists; }
            set { SetProperty(ref lists, value); }
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
                Lists.Clear(); // Clear existing lists if refreshing all data
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
            if (SelectedList != null)
            {
                var taskDescription = await _dialogService.GetTaskDescriptionAsync();
                var taskStatus = await _dialogService.GetTaskStatusAsync();
                var taskResponsibility = await _dialogService.GetTaskResponsibilityAsync();

                try
                {
                    await _apiService.AddTaskToList(SelectedList.Id, taskDescription, taskStatus, taskResponsibility);
                    await LoadData(); // Refresh the data after adding the task
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error adding task: {ex.Message}");
                    // Handle the exception as needed
                }
            }
        }

    }


}



