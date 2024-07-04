using System.ComponentModel.DataAnnotations;

namespace QuickBank.Core.Application.ViewModels.Facilities.Benefice
{
    public class BeneficeSaveViewModel
    {
        [Required(ErrorMessage = "The number account field cannot be empty")]
        [DataType(DataType.Text)]
        public string NumberAccount { get; set; }
    }
}
