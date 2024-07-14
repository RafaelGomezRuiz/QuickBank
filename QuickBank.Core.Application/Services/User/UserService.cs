using AutoMapper;
using QuickBank.Core.Application.Dtos.Account;
using QuickBank.Core.Application.Enums;
using QuickBank.Core.Application.Interfaces.Services.User;
using QuickBank.Core.Application.Services.Products;
using QuickBank.Core.Application.ViewModels.Auth;
using QuickBank.Core.Application.ViewModels.User;

namespace QuickBank.Core.Application.Services.User
{
    public class UserService : IUserService
    {
        protected readonly IAccountService _accountService;
        protected readonly IMapper _mapper;
        public UserService(IAccountService _accountService, IMapper _mapper)
        {
            this._accountService = _accountService;
            this._mapper = _mapper;
        }
        public async Task<bool> DuplicateUserName(string userName)
        {
            return await _accountService.DuplicateUserName(userName);
        }
        public async Task<bool> DuplicateEmail(string email)
        {
            return await _accountService.DuplicateEmail(email);
        }

        public async Task<IEnumerable<UserViewModel>> GetActiveUsersAsync()
        {
            return (await GetAllAsync()).Where(user=>user.Status==(int)EUserStatus.ACTIVE && user.Roles[0] ==ERoles.BASIC.ToString());
        }
        public async Task<IEnumerable<UserViewModel>> GetInactiveUsersAsync()
        {
            return (await GetAllAsync()).Where(user => user.Status == (int)EUserStatus.INACTIVE && user.Roles[0] == ERoles.BASIC.ToString());
        }
        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            var usersResponse= await _accountService.GetAllAsync();
            var usersReturn=_mapper.Map<IEnumerable<UserViewModel>>(usersResponse);
            return usersReturn;
        }
        public async Task<UserSaveViewModel> FindyByIdAsync(string id)
        {
            AuthenticationResponse response = await _accountService.FindByIdAsync(id);
            UserSaveViewModel user = _mapper.Map<UserSaveViewModel>(response);
            return user;
        }

        public async Task<RegisterResponse> RegisterAsync(UserSaveViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            RegisterResponse registerResponse = await _accountService.RegisterUserAsync(registerRequest, origin);
            return registerResponse;
        }

        public async Task<UserSaveViewModel> UpdateUserAsync(UserSaveViewModel saveUserViewModel)
        {
            AuthenticationResponse response = _mapper.Map<AuthenticationResponse>(saveUserViewModel);
            response = await _accountService.UpdateUserAsync(response);
            UserSaveViewModel userUpdated = _mapper.Map<UserSaveViewModel>(response);
            return userUpdated;
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest authenticationRequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse authenticationResponse = await _accountService.AuthenticateAsync(authenticationRequest);
            return authenticationResponse;
        }

        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }

        //public async Task<string> ConfirmEmailAsync(string userId, string token)
        //{
        //    return await _accountService.ConfirmAccountAsync(userId, token);
        //}

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel forgotPasswordVm, string origin)
        {
            ForgotPasswordRequest forgotPasswordRequest = _mapper.Map<ForgotPasswordRequest>(forgotPasswordVm);
            ForgotPasswordResponse forgotPasswordResponse = await _accountService.ForgotPasswordAsync(forgotPasswordRequest, origin);
            return forgotPasswordResponse;
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel resetPassword)
        {
            ResetPasswordRequest resetPasswordRequest = _mapper.Map<ResetPasswordRequest>(resetPassword);
            return await _accountService.ResetPasswordAsync(resetPasswordRequest);
        }


    }
}
