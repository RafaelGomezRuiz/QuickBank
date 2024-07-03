namespace QuickBank.Core.Application.Helpers
{
    public static class EnvironmentVariablesHelper
    {
        public static string? GetValue(string key)
        {
            return Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.User);
        }
    }
}
