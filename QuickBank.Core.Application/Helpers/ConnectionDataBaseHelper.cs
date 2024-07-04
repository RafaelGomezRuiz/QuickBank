using Microsoft.Extensions.Configuration;

namespace QuickBank.Core.Application.Helpers
{
    public static class ConnectionDataBaseHelper
    {
        public static string GetConnectionStringWithEnviormentVariable(this IConfiguration config,string nameConnection, string enviormentVariable)
        {
            var connection = config.GetConnectionString(nameConnection);

            string[] connectionStringArray = connection.Split('.');
            connectionStringArray[0] += Environment.GetEnvironmentVariable(enviormentVariable, EnvironmentVariableTarget.User);
            return string.Join("", connectionStringArray);
        }
    }
}
