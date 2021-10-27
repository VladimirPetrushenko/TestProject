using Microsoft.AspNetCore.Authorization;

namespace MyFirstTestProject.Features.Authorization
{
    public class OnlyEmployeesRequirement : IAuthorizationRequirement
    {
    }
}
