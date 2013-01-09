namespace Covariance
{
    public interface IBuilder<in TIn, out T>
    {
        T Build();
        string SayType(TIn @in);
    }

    public class Builder<TIn,TOut> : IBuilder<TIn,TOut>
    {
        public TOut Build()
        {
            return default(TOut);
        }

        public string SayType(TIn @in)
        {
            return typeof(TIn).Name+" : "+@in.GetType().Name;
        }
    }
}