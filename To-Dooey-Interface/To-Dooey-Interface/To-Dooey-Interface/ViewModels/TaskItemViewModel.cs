using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace To_Dooey_Interface.ViewModels
{
    public class TaskItemViewModel : INotifyPropertyChanged
    {
        private string _description;
        private CompletionStatus _status;
        private string _responsibility;
        private bool _iscompleted;
        private bool _isSelected;

        public int Id { get; set; }


        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
        [JsonPropertyName("description")]
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        [JsonPropertyName("status")]
        public CompletionStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        [JsonPropertyName("responsibility")]
        public string Responsibility
        {
            get => _responsibility;
            set => SetProperty(ref _responsibility, value);
        }

        [JsonPropertyName("iscompleted")]
        public bool IsCompleted
        {
            get => _iscompleted;
            set => SetProperty(ref _iscompleted, value);
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
