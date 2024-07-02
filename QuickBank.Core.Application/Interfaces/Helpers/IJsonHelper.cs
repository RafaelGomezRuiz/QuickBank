namespace QuickBank.Core.Application.Interfaces.Helpers
{
    public interface IJsonHelper
    {
        string Serialize<T>(T value);
        T? Deserialize<T>(string json);
    }
}
