using AutoMapper;
using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Application.Interfaces.Services;
using QuickBank.Core.Application.ViewModels.Facilities.Benefice;
using QuickBank.Core.Application.ViewModels.Payments;
using QuickBank.Core.Application.ViewModels.Products;
using QuickBank.Core.Domain.Entities;
using QuickBank.Core.Domain.Entities.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBank.Core.Application.Services
{
    public class BeneficeService : GenericService<BeneficeSaveViewModel, BeneficeViewModel, BeneficeEntity>, IBeneficeService
    {
        protected readonly IBeneficeRepository _beneficeRepository;
        protected readonly IMapper _mapper;

        public BeneficeService(IBeneficeRepository _beneficeRepository, IMapper _mapper) : base(_beneficeRepository, _mapper) 
        {
            this._beneficeRepository = _beneficeRepository;
            this._mapper = _mapper;
        }
    }
}
