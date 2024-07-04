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
    public class LoanService : GenericService<LoanPaySaveViewModel, LoanViewModel, LoanEntity>,ILoanService
    {
        protected readonly ILoanRepository _loanRepository;
        protected readonly IMapper _mapper;

        public LoanService(ILoanRepository _loanRepository, IMapper _mapper) : base(_loanRepository, _mapper)
        {
            this._loanRepository = _loanRepository;
            this._mapper = _mapper;
        }
    }
}
