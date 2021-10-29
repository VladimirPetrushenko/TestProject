using Microsoft.AspNetCore.Authorization;

namespace MyApi.ApiKey.Authorization
{
    public class OnlyEmployeesRequirement : IAuthorizationRequirement
    {
    }
}
