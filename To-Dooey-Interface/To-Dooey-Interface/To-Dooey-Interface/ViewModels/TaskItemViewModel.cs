// TaskItemViewModel.cs
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using To_Dooey_Interface.Models;

namespace To_Dooey_Interface.ViewModels
{
    public partial class TaskItemViewModel : INotifyPropertyChanged
    {
        private string _description;
        private CompletionStatus _status;
        private string _responsibility;
        private bool _iscompleted;

        public int Id { get; set; }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public CompletionStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }
        public bool IsCompleted
        {
            get => _iscompleted;
            set => SetProperty(ref _iscompleted, value);
        }


        public string Responsibility
        {
            get => _responsibility;
            set => SetProperty(ref _responsibility, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
