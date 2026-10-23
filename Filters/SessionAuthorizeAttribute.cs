
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RaceDayAPI.Filters
{
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Method)]
    public class SessionAuthorizeAttribute : Attribute,
        IAuthorizationFilter
    {
        private readonly string? _requiredRole;

        public SessionAuthorizeAttribute(
            string? requiredRole = null)
        {
            _requiredRole = requiredRole;
        }

        public void OnAuthorization(
            AuthorizationFilterContext context)
        {
            var session = context.HttpContext.Session;

            int? userId = session.GetInt32("UserID");

            string? userRole =
                session.GetString("UserRole");

            // User is not logged in
            if (userId == null)
            {
                context.Result = new UnauthorizedObjectResult(
                    new
                    {
                        Message = "Please log in to continue."
                    });

                return;
            }

            // User does not have the required role
            if (_requiredRole != null &&
                userRole != _requiredRole)
            {
                context.Result = new ObjectResult(
                    new
                    {
                        Message = "Access denied. Insufficient permissions."
                    })
                {
                    StatusCode = 403
                };
            }
        }
    }
}
