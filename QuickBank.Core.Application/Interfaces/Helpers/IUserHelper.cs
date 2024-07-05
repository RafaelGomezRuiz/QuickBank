using QuickBank.Core.Application.Dtos.Account;

namespace QuickBank.Core.Application.Interfaces.Helpers
{
    public interface IUserHelper
    {
        public void SetUser(AuthenticationResponse user);
        public AuthenticationResponse? GetUser();
        public void RemoveUser();
        public bool HasUser();
    }
}
