using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using QuickBank.Core.Application.Dtos.Account;
using QuickBank.Core.Application.Interfaces.Helpers;

namespace QuickBank.Core.Application.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly string userKeySession = string.Empty;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration config;

        public UserHelper(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            userKeySession = config.GetSection("SessionConfig").GetValue<string>("UserInfoKey")!;
            this.httpContextAccessor = httpContextAccessor;
            this.config = config;
        }

        public void SetUser(AuthenticationResponse user)
        {
            httpContextAccessor.HttpContext.Session.Set(userKeySession, user);
        }

        public AuthenticationResponse? GetUser()
        {
            return httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>(userKeySession);
        }

        public void RemoveUser()
        {
            httpContextAccessor.HttpContext.Session.Remove(userKeySession);
        }

        public bool HasUser()
        {
            return GetUser() != null;
        }
    }
}
