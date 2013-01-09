using System.ComponentModel;

namespace NotifyPropChangedIL.ViewModels
{
    public class MainViewModel:INotifyPropertyChanged
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}",FirstName,LastName); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}