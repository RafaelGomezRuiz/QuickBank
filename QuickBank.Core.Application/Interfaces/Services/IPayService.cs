﻿using QuickBank.Core.Application.ViewModels.Payments;

namespace QuickBank.Core.Application.Interfaces.Services
{
    public interface IPayService
    {
        Task MakeExpressPay(ExpressPaySaveViewModel epsvm);
    }
}
