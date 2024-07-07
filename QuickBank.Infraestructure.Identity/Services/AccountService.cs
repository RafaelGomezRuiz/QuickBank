using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using QuickBank.Core.Application.Dtos.Account;
using QuickBank.Core.Application.Dtos.Email;
using QuickBank.Core.Application.Enums;
using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Services.Facilities;
using QuickBank.Core.Application.Interfaces.Services.User;
using QuickBank.Infrastructure.Identity.Entities;
using System.Text;

namespace QuickBank.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        protected readonly UserManager<ApplicationUser> userManager;
        protected readonly SignInManager<ApplicationUser> signInManager;
        protected readonly IEmailService emailService;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailService = emailService;
        }
        public async Task<AuthenticationResponse> FindByIdAsync(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            AuthenticationResponse response = new();

            response.Id = user.Id;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.UserName = user.UserName;
            response.Email = user.Email;
            response.PhoneNumber = user.PhoneNumber;
            response.Status = user.Status;
            response.IdCard = user.IdCard;
            var rolesList = await userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            return response;
        }
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();
            ApplicationUser? user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.ErrorDescription = $"No accounts with this {request.Email}";
                return response;
            }
            var result = await signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.ErrorDescription = $"Invalid credentials for {request.Email}";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.ErrorDescription = $"Acount not confirmed for {request.Email}";
                return response;
            }

            response.Id = user.Id;
            response.UserName = user.UserName;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName ;
            response.Email = user.Email;
            response.IdCard = user.IdCard;
            response.PhoneNumber = user.PhoneNumber;

            var rolesList = await userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            response.Status = user.Status;

            return response;
        }

        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var DuplicateUserName = await userManager.FindByNameAsync(request.UserName);
            if (DuplicateUserName != null)
            {
                response.HasError = true;
                response.ErrorDescription = $"this UserName '{request.UserName}' in use";
                return response;
            }

            var DuplicateEmail = await userManager.FindByEmailAsync(request.Email);
            if (DuplicateEmail != null)
            {
                response.HasError = true;
                response.ErrorDescription = $"this Email '{request.Email}' is already registered";
                return response;
            }
            
            ApplicationUser user = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.Email,
                IdCard = request.IdCard,
                PhoneNumber = request.PhoneNumber, 
            };

            var userCreated = await userManager.CreateAsync(user, request.Password);
            if (userCreated.Succeeded)
            {
                await userManager.AddToRoleAsync(user, ERoles.BASIC.ToString());
                var verificationUri = await SendVerificationEmailUri(user, origin);
                await emailService.SendAsync(new EmailRequest
                {
                    To = user.Email,
                    Body = $"Please confirm your acount visisting this link: {verificationUri}",
                    Subject = "Confirm registration"
                });
            }
            else
            {
                response.HasError = true;
                response.ErrorDescription = $"Has ocurred an error trying to save the user";
                return response;
            }
            return response;
        }
        //LeftJoinExpression the method to AdminUser

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            ApplicationUser user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No accounts registered with this user";
            }
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirmed for {user.UserName}. Yuo can use the app";
            }
            else
            {
                return $"An error occured while confirming {user.UserName} try again";
            }
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new()
            {
                HasError = false
            };
            ApplicationUser user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.HasError = true;
                response.ErrorDescription = $"There are no user registered with this '{request.Email}' emailaddress";
                return response;
            }

            var code = await userManager.GeneratePasswordResetTokenAsync(user);
            var newPassword = PasswordGenerator.GeneratePassword();
            var result = await userManager.ResetPasswordAsync(user, code, newPassword);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.ErrorDescription = $"An error has ocurred resetting the password try again";
                return response;
            }
            var LoginAddress = await SendForgotPasswordUri(origin);
            await emailService.SendAsync(new EmailRequest()
            {
                To = user.Email,
                Body = $"Reset successfully, this is your new password: {newPassword} Click here to join the app! {LoginAddress}",
                Subject = "Password Resseted"
            });
            return response;
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new()
            {
                HasError = false
            };
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.ErrorDescription = $"No accounts registered with this '{request.Email}' email address ";
                return response;
            }
            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.ErrorDescription = $"An error has ocurred rsetting the password try again";
                return response;
            }
            return response;
        }

        public async Task<AuthenticationResponse> UpdateUserAsync(AuthenticationResponse responseUserVm)
        {
            AuthenticationResponse response = new()
            {
                HasError = false
            };
            ApplicationUser userToUpdate = await userManager.FindByIdAsync(responseUserVm.Id);

            if (userToUpdate == null)
            {
                response.HasError = true;
                response.ErrorDescription = "User not found";
                return response;
            }

            if (userToUpdate.UserName != responseUserVm.UserName)
            {
                var DuplicateUserName = await userManager.FindByNameAsync(responseUserVm.UserName);
                if (DuplicateUserName != null)
                {
                    response.HasError = true;
                    response.ErrorDescription = $"this UserName '{responseUserVm.UserName}' in use";
                    return response;
                }
            }

            if (userToUpdate.Email != responseUserVm.Email)
            {
                var DuplicateEmail = await userManager.FindByEmailAsync(responseUserVm.Email);
                if (DuplicateEmail != null)
                {
                    response.HasError = true;
                    response.ErrorDescription = $"this Email '{responseUserVm.Email}' is already registered";
                    return response;
                }
            }

            userToUpdate.Id = responseUserVm.Id;
            userToUpdate.FirstName = responseUserVm.FirstName;
            userToUpdate.LastName = responseUserVm.LastName;
            userToUpdate.UserName = responseUserVm.UserName;
            userToUpdate.IdCard = responseUserVm.IdCard;
            userToUpdate.Status = responseUserVm.Status;
            userToUpdate.Email = responseUserVm.Email;
            userToUpdate.PhoneNumber = responseUserVm.PhoneNumber;

            var userUpdatedSuccessfully = await userManager.UpdateAsync(userToUpdate);
            if (!userUpdatedSuccessfully.Succeeded)
            {
                response.HasError = true;
                response.ErrorDescription = "An ocurred an error updating the user try again";
                return response;
            }
            ApplicationUser userUpdated = await userManager.FindByIdAsync(responseUserVm.Id);
            response.Id = userUpdated.Id;
            response.UserName = userUpdated.UserName;
            response.Email = userUpdated.Email;
            response.PhoneNumber = userUpdated.PhoneNumber;
            var rolesList = await userManager.GetRolesAsync(userUpdated).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = userUpdated.EmailConfirmed;

            return response;
        }

        private async Task<string> SendForgotPasswordUri(string origin)
        {
            var route = "User/Index";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = Uri.ToString();
            return verificationUri;
        }

        private async Task<string> SendVerificationEmailUri(ApplicationUser user, string origin)
        {
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);
            return verificationUri;
        }

    }
}
