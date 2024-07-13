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
            var userBeneficiaries = await beneficeService.GetBeneficiariesWithFullNameAsync();
            return View(userBeneficiaries);
        }

        [Authorize(Roles = "BASIC")]
        public IActionResult AddBeneficiary()
        {
            return View(new BeneficeSaveViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "BASIC")]
        public async Task<IActionResult> AddBeneficiary(BeneficeSaveViewModel model)
        {
            ModelState.AddModelErrorRange(await facilityValidationService.ValidateBeneficiary(model));

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await beneficeService.AddAsync(model);
            return RedirectToAction("BeneficeHome");
        }

        [Authorize(Roles = "BASIC")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var beneficiaryViewModel = await beneficeService.GetBeneficiaryByIdAsync(id);
            if (beneficiaryViewModel == null)
            {
                return NotFound();
            }
            return View(beneficiaryViewModel);
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
