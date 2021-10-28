using Microsoft.AspNetCore.Authorization;

namespace MyApi.Configuration.Authorization
{
    public class OnlyEmployeesRequirement : IAuthorizationRequirement
    {
    }
}
