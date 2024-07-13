using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBank.Core.Application.Interfaces.Helpers;
using QuickBank.Core.Application.Interfaces.Services.Facilities;
using QuickBank.Core.Application.ViewModels.Facilities.Benefice;
using QuickBank.Helpers;

namespace QuickBank.Controllers
{
    [Authorize(Roles = "BASIC")]
    public class BeneficeController : Controller
    {
        private readonly IBeneficeService beneficeService;
        private readonly IFacilityValidationService facilityValidationService;
        private readonly IUserHelper userHelper;

        public BeneficeController(
            IBeneficeService beneficeService,
            IFacilityValidationService facilityValidationService,
            IUserHelper userHelper)
        {
            this.beneficeService = beneficeService;
            this.facilityValidationService = facilityValidationService;
            this.userHelper = userHelper;
        }

        [Authorize(Roles = "BASIC")]
        public async Task<IActionResult> BeneficeHome()
        {
            return View(await beneficeService.GetAllByUserIdWithFullNameAsync(userHelper.GetUser()!.Id));
        }

        [Authorize(Roles = "BASIC")]
        public IActionResult AddBenefice()
        {
            return View(new BeneficeSaveViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "BASIC")]
        public async Task<IActionResult> AddBenefice(BeneficeSaveViewModel bsvm)
        {
            // Validate before the add
            ModelState.AddModelErrorRange(await facilityValidationService.BeneficeValidation(bsvm));
            if (!ModelState.IsValid) return View(bsvm);

            // Add the benefice
            await beneficeService.AddAsync(bsvm);

            return RedirectToAction("BeneficeHome");
        }

        [Authorize(Roles = "BASIC")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            return View(await beneficeService.GetByIdWithFullNameAsync(id));
        }

        [HttpPost]
        [Authorize(Roles = "BASIC")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await beneficeService.DeleteAsync(id);
            return RedirectToAction("BeneficeHome");
        }
    }
}
