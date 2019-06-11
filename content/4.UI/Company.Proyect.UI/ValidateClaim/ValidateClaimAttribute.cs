namespace Company.Proyect.UI.ValidateClaim
{
    using Infra.Utils.Security;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;

    [AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class ValidateClaimAttribute : Attribute, IAuthorizationFilter
    {
        public string Template { get; }

        public ValidateClaimAttribute(string template)
        {
            this.Template = template;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var controller = (context.ActionDescriptor as ControllerActionDescriptor).ControllerName.ToLower();
            var claimValue = this.Template.Replace("[controller]", controller);
            if (context.HttpContext.User.HasClaim(claim =>
                claim.Type == CustomClaimTypes.Permission && claim.Value == claimValue))
            {
                return;
            }
            context.Result = new ContentResult { Content = "No estás autorizado", StatusCode = 401 };
        }
    }
}
