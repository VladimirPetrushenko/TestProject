using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace MyApi.ApiKey.Authorization
{
    public class OnlyEmployeesAuthorizationHandler : AuthorizationHandler<OnlyEmployeesRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OnlyEmployeesRequirement requirement)
        {
            if (context.User.IsInRole(Roles.Employee))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
