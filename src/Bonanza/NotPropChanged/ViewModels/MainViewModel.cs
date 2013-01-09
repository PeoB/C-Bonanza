namespace NotPropChanged.ViewModels
{
    public class MainViewModel:BindableBase
    {
        public string FirstName { 
            get { return Get(); } 
            set { Set(value); } 
        }

        public string LastName
        {
            get { return Get(); }
            set { Set(value); }
        }

        public string FullName
        {
            get { return string.Format("{0} {1}", Get(() => FirstName), Get(() => LastName)); }
        }
    }
}