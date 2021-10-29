using Microsoft.AspNetCore.Authorization;

namespace MyApi.ApiKey.Authorization
{
    public class OnlyManagersRequirement : IAuthorizationRequirement
    {
    }
}
