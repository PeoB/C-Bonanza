namespace Covariance
{
    public interface IQuack
    {
        string Quack();
    }

    public class Duck : IQuack
    {
        public string Quack()
        {
            return "Quack";
        }
    }

    public class Fallout:IQuack
    {
        public string Quack()
        {
            return "Quack I say";
        }
    }
}