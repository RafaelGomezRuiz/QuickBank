﻿using AutoMapper;
using QuickBank.Core.Application.Dtos.Account;
using QuickBank.Core.Application.ViewModels.Auth;
using QuickBank.Core.Application.ViewModels.Facilities.Benefice;
using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Core.Application.ViewModels.User;
using QuickBank.Core.Domain.Entities.Facilities;
using QuickBank.Core.Domain.Entities.Productos;

namespace QuickBank.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Authentication
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(destino => destino.HasError, otp => otp.Ignore())
                .ForMember(destino => destino.ErrorDescription, otp => otp.Ignore())
                .ReverseMap();
            #endregion

            #region User

            CreateMap<UserViewModel, AuthenticationResponse>()
                .ForMember(destino => destino.HasError, otp => otp.Ignore())
                .ForMember(destino => destino.ErrorDescription, otp => otp.Ignore())
                .ForMember(destino => destino.IsVerified, otp => otp.Ignore())
                .ReverseMap()
                .ForMember(destino => destino.Password, otp => otp.Ignore());

            CreateMap<UserSaveViewModel, RegisterRequest>()
                .ReverseMap()
                .ForMember(destino => destino.HasError, otp => otp.Ignore())
                .ForMember(destino => destino.ErrorDescription, otp => otp.Ignore());

            CreateMap<UserSaveViewModel, AuthenticationResponse>()
                .ForMember(destino => destino.PhoneNumber, otp => otp.Ignore())
                .ReverseMap()
                .ForMember(destino => destino.Password, otp => otp.Ignore())
                .ForMember(destino => destino.ConfirmPassword, otp => otp.Ignore());

            #endregion

            #region Products

            CreateMap<SavingAccountEntity, SavingAccountViewModel>()
                .ForMember(destino => destino.OwnerId, otp => otp.MapFrom(src => src.UserId))
                .ReverseMap()
                .ForMember(destino => destino.UserId, otp => otp.MapFrom(src => src.OwnerId));

            CreateMap<CreditCardEntity, CreditCardViewModel>()
                .ForMember(destino => destino.OwnerId, otp => otp.MapFrom(src => src.UserId))
                .ReverseMap()
                .ForMember(destino => destino.UserId, otp => otp.MapFrom(src => src.OwnerId));

            CreateMap<LoanEntity, LoanViewModel>()
                .ForMember(destino => destino.OwnerId, otp => otp.MapFrom(src => src.UserId))
                .ReverseMap()
                .ForMember(destino => destino.UserId, otp => otp.MapFrom(src => src.OwnerId));


            #endregion

            #region Facilities

            CreateMap<BeneficeEntity, BeneficeViewModel>()
                .ForMember(b => b.BenefitedFullName, opt => opt.Ignore())
                .ReverseMap();

            #endregion

        }
    }
}
