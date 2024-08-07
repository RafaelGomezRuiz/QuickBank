﻿using Microsoft.AspNetCore.Mvc;

namespace QuickBank.Core.Application.Helpers
{
    public static class RedirectRoutesHelper
    {
        public static RedirectToRouteResult routeBasicHome = new RedirectToRouteResult(new {controller = "Home", action = "BasicHome" });
        public static RedirectToRouteResult routeAdminHome = new RedirectToRouteResult(new { controller = "Home", action = "AdminHome" });
        public static RedirectToRouteResult routeAdmininistrationUserIndex = new RedirectToRouteResult(new { controller = "AdministrationUser", action = "Index" });
        public static RedirectToRouteResult routeAdmininistrationUserProducts = new RedirectToRouteResult(new { controller = "AdministrationUser", action = "UserProducts" });

        public static RedirectToRouteResult routeUndefiniedHome = new RedirectToRouteResult(new { controller = "", action = "" });
    }
}
