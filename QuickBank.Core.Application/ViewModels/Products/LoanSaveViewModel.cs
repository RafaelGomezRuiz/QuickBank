using System.ComponentModel.DataAnnotations;

namespace QuickBank.Core.Application.ViewModels.Products
{
    public class LoanSaveViewModel
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public double Amount { get; set; }

        [Required(ErrorMessage ="La descripcion es requerida")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

    }
}
