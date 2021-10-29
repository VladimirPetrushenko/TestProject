using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace MyApi.ApiKey.Authorization
{
    public class OnlyManagersAuthorizationHandler : AuthorizationHandler<OnlyManagersRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OnlyManagersRequirement requirement)
        {
            if (context.User.IsInRole(Roles.Manager))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
