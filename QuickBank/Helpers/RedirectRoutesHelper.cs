using Microsoft.AspNetCore.Mvc;

namespace QuickBank.Core.Application.Helpers
{
    public static class RedirectRoutesHelper
    {
        public static RedirectToRouteResult routeBasicHome = new RedirectToRouteResult(new {controller = "Home", action = "BasicHome" });
        public static RedirectToRouteResult routeAdminHome = new RedirectToRouteResult(new { controller = "Home", action = "AdminHome" });
        public static RedirectToRouteResult routeUndefiniedHome = new RedirectToRouteResult(new { controller = "", action = "" });
    }
}
