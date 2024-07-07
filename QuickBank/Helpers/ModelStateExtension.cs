using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace QuickBank.Helpers
{
    public static class ModelStateExtension
    {
        public static void AddModelErrorRange(this ModelStateDictionary modelState, Dictionary<string, string> errors)
        {
            foreach (var error in errors) modelState.AddModelError(error.Key,error.Value);
        }
    }
}
