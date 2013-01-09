using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using ExtensionMethods;
namespace NotPropChanged.ViewModels
{
    /// <summary>
    /// Implementation of <see cref="INotifyPropertyChanged"/> to simplify models.
    /// </summary>
    [Windows.Foundation.Metadata.WebHostHidden]
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IDictionary<string, object> _properties = new Dictionary<string, object>();
        private readonly IDictionary<string, HashSet<string>> _dependantProperties = new Dictionary<string, HashSet<string>>();

        protected bool Set<T>(T value, [CallerMemberName] string propertyName = null)
        {
            object old;
            if (!_properties.TryGetValue(propertyName, out old)) old = null;
            if (Equals(old, value)) return false;

            _properties[propertyName] = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected dynamic Get([CallerMemberName] string propertyName = null)
        {
            object value;
            return _properties.TryGetValue(propertyName, out value) ? value : null;
        }

        protected T Get<T>(Expression<Func<T>> expression, [CallerMemberName]string propertyName = null)
        {
            var targetProperty = ((MemberExpression)expression.Body).Member.Name;

            _dependantProperties
                .SafeUpdate(
                    targetProperty,
                    set => set.Tap(s => s.Add(propertyName)),
                    new HashSet<string>());
            return expression.Compile()();
        }




        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
                _dependantProperties.SafeGet(propertyName, new HashSet<string>()).ForEach(OnPropertyChanged);
            }
        }
    }


}