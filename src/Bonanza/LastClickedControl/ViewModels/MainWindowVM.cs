using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using LastClickedControl.win32;

namespace LastClickedControl.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        private readonly Hook _mouseHook;
        public MainWindowVM()
        {
            _mouseHook = new Hook();
            _mouseHook.MouseButtonDown += SetTitle;
            _mouseHook.HookMouse();
        }

        private void SetTitle(object sender, MouseEventArgs mouseEventArgs)
        {
            var title = Control.FindByLocation(mouseEventArgs.Point).Title;
            Application.Current.Dispatcher.BeginInvoke(new Action(() => Title = string.IsNullOrEmpty(title) ? "No title found" : title));
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value) return;
                _title = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Title"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }
    }
}