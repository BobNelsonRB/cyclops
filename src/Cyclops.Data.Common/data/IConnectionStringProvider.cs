namespace Cyclops.Data.Common
{
    public interface IConnectionStringProvider
    {
        string Get(string key);
        string Get();
        string Get<T>() where T : class, new();
    }
}
