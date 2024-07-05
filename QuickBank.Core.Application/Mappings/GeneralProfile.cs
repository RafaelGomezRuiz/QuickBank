using AutoMapper;
using QuickBank.Core.Application.Dtos.Account;
using QuickBank.Core.Application.ViewModels.Auth;

namespace QuickBank.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(destino => destino.HasError, otp => otp.Ignore())
                .ForMember(destino => destino.ErrorDescription, otp => otp.Ignore())
                .ReverseMap();
        }
    }
}
