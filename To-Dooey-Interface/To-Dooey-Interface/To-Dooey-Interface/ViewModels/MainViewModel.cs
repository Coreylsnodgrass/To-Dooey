using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace To_Dooey_Interface.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _title = "Welcome to To-Dooey";

        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
