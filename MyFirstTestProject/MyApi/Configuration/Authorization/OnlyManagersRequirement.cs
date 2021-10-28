using Microsoft.AspNetCore.Authorization;

namespace MyApi.Configuration.Authorization
{
    public class OnlyManagersRequirement : IAuthorizationRequirement
    {
    }
}
