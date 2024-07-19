using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QuickBank.Core.Application.Dtos;
using QuickBank.Core.Application.Dtos.Account;
using QuickBank.Core.Application.Enums;
using QuickBank.Core.Application.Interfaces.Services.Facilities;
using QuickBank.Core.Application.Interfaces.Services.Products;
using QuickBank.Core.Application.Interfaces.Services.User;
using QuickBank.Infrastructure.Identity.Entities;

namespace QuickBank.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
        private readonly ISavingAccountService savingAccountService;


        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
            ISavingAccountService savingAccountService,
            IEmailService emailService
        )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.savingAccountService = savingAccountService;
            this.emailService = emailService;
            this.savingAccountService = savingAccountService;
        }


        public async Task<IEnumerable<AuthenticationResponse>> GetAllAsync()
        {
            var applicationUsers = userManager.Users.ToList();
            var authResponses = new List<AuthenticationResponse>();

            foreach (var user in applicationUsers)
            {
                var rolesList = await userManager.GetRolesAsync(user).ConfigureAwait(false);
                var response = mapper.Map<AuthenticationResponse>(user);
                response.Roles = rolesList.ToList();
                authResponses.Add(response);
            }

            return authResponses;
        }

        public async Task<AuthenticationResponse> FindByIdAsync(string userId)
        {
            var applicationUser = await userManager.FindByIdAsync(userId);
            var response = mapper.Map<AuthenticationResponse>(applicationUser);
            response.Roles = (await userManager.GetRolesAsync(applicationUser!).ConfigureAwait(false)).ToList();

            return response;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            // Resouces
            var applicationUser = await userManager.FindByEmailAsync(request.Email);
            var responseWithErrors = new AuthenticationResponse();

            if (applicationUser == null)
            {
                responseWithErrors.HasError = true;
                responseWithErrors.ErrorDescription = $"No accounts with this {request.Email}";
                return responseWithErrors;
            }

            var resultCredential = await signInManager.PasswordSignInAsync(applicationUser.UserName, request.Password, false, false);
            if (!resultCredential.Succeeded)
            {
                responseWithErrors.HasError = true;
                responseWithErrors.ErrorDescription = $"Invalid credentials for {request.Email}";
                return responseWithErrors;
            }

            if (!applicationUser.EmailConfirmed)
            {
                responseWithErrors.HasError = true;
                responseWithErrors.ErrorDescription = $"Acount not confirmed for {request.Email}";
                return responseWithErrors;
            }

            if (applicationUser.Status == (int)EUserStatus.INACTIVE)
            {
                responseWithErrors.HasError = true;
                responseWithErrors.ErrorDescription = $"Acount inactive for {request.Email}, please communicate with the admin";
                return responseWithErrors;
            }

            var responseWithData = mapper.Map<AuthenticationResponse>(applicationUser);
            responseWithData.Roles = (await userManager.GetRolesAsync(applicationUser).ConfigureAwait(false)).ToList();

            return responseWithData;
        }

        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<bool> DuplicateUserName(string userName)
        {
            return (await userManager.FindByNameAsync(userName)) != null;
        }

        public async Task<bool> DuplicateEmail(string email)
        {
            return (await userManager.FindByEmailAsync(email)) != null;
        }

        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request)
        {
            // Resources
            RegisterResponse response = new();
            ApplicationUser userToRegister = mapper.Map<ApplicationUser>(request);

            // Dafault values for user when is created
            userToRegister.EmailConfirmed = true;
            userToRegister.PhoneNumberConfirmed = true;
            userToRegister.Status = (int)EUserStatus.ACTIVE;

            // Try to create the user
            var resultCreation = await userManager.CreateAsync(userToRegister, request.Password);
            if (!resultCreation.Succeeded)
            {
                response.HasError = true;
                response.ErrorDescription = $"Has ocurred an error trying to save the user";
                return response;
            }

            // Set roles for created user
            await userManager.AddToRoleAsync(userToRegister, request.UserType.ToString());

            // Assing saving account if the user created is a basic user
            if (request.UserType == ERoles.BASIC)
            {
                SetSavingAccount setSavingAccount = new()
                {
                    UserId = userToRegister.Id,
                    InitialAmount = (double)request.InitialAmount!
                };
                await savingAccountService.SetSavingAccount(setSavingAccount);
            }

            // Set id of user creatde to the response
            response.Id = userToRegister.Id;

            return response;
        }


        public async Task<AuthenticationResponse> UpdateUserAsync(AuthenticationResponse request)
        {
            // Resources
            var response = new AuthenticationResponse();
            ApplicationUser userToUpdate = await userManager.FindByIdAsync(request.Id);

            userToUpdate.Id = request.Id;
            userToUpdate.FirstName = request.FirstName;
            userToUpdate.LastName = request.LastName;
            userToUpdate.UserName = request.UserName;
            userToUpdate.IdCard = request.IdCard;
            userToUpdate.Status = request.Status;
            userToUpdate.Email = request.Email;
            userToUpdate.PhoneNumber = request.PhoneNumber;
            
            // Credit the balance from the request in the principal saving account
            if (request.UserType == nameof(ERoles.BASIC))
            {
                var principalSavingAccount = await savingAccountService.GetPrincipalSavingAccountAsync(request.Id);
                principalSavingAccount.Balance += (double)request.InitialAmount!;
                await savingAccountService.UpdateAsync(principalSavingAccount, principalSavingAccount.Id);
            }

            // Try to update the user
            var resultUpdate = await userManager.UpdateAsync(userToUpdate);
            if (!resultUpdate.Succeeded)
            {
                response.HasError = true;
                response.ErrorDescription = "An ocurred an error updating the user try again";
                return response;
            }

            // Fill the response with data
            response = mapper.Map<AuthenticationResponse>(userToUpdate);
            response.Roles = (await userManager.GetRolesAsync(userToUpdate).ConfigureAwait(false)).ToList();

            return response;
        }

    }
}
