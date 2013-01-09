using System.ComponentModel;

namespace NotPropChangedIL.ViewModels
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

    }
}