using AutoMapper;
using QuickBank.Core.Application.Dtos.Account;
using QuickBank.Core.Application.ViewModels.Auth;
using QuickBank.Core.Application.ViewModels.Facilities.Benefice;
using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Core.Domain.Entities;
using QuickBank.Core.Domain.Entities.Productos;

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


            #region Products

            CreateMap<SavingAccountEntity, SavingAccountViewModel>()
                .ReverseMap();



            #endregion


            #region Facilities

            CreateMap<BeneficeEntity, BeneficeViewModel>()
                .ForMember(b => b.BenefitedFullName, opt => opt.Ignore())
                .ReverseMap();

            #endregion
        
        }
    }
}
