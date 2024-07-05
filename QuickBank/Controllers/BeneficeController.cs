using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuickBank.Controllers
{
    [Authorize(Roles = "BASIC")]
    public class BeneficeController : Controller
    {

    }
}
