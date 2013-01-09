using System.ComponentModel;

namespace NotifyPropChangedIL.ViewModels
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

    }
}