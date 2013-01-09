namespace NotPropChangedIL.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public string GivenNames { get; set; }
        public string FamilyName { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", GivenNames, FamilyName);
            }
        }
    }
}