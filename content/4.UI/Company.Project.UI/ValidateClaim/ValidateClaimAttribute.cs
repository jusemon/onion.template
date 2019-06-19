namespace Company.Project.UI.ValidateClaim
{
    using Infra.Utils.Security;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;

    /// <summary>
    /// Validate Claim Attribute class. 
    /// </summary>
    /// <seealso cref="System.Attribute" />
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter" />
    [AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class ValidateClaimAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// Gets the template.
        /// </summary>
        /// <value>
        /// The template.
        /// </value>
        public string Template { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateClaimAttribute"/> class.
        /// </summary>
        /// <param name="template">The template.</param>
        public ValidateClaimAttribute(string template)
        {
            this.Template = template;
        }

        /// <summary>
        /// Called early in the filter pipeline to confirm request is authorized.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext" />.</param>
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
