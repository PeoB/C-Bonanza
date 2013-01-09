namespace Covariance
{
    class Program
    {
        static void Main(string[] args)
        {
            IBuilder<IQuack,IQuack> builder = new Builder<IQuack,Duck>();
            
            builder.SayType(new Fallout());
        }
    }
}
