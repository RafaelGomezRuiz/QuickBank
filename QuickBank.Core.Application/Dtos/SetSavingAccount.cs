using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBank.Core.Application.Dtos
{
    public class SetSavingAccount
    {
        public string UserId { get; set; }
        public double? InitialAmount { get; set; }
    }
}
