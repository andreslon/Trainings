namespace Excelsior.Infrastructure.Interfaces
{
    public interface ISettings
    {
        string GetSetting(string key);
        string GetConnectionString(string key);
    }
}
