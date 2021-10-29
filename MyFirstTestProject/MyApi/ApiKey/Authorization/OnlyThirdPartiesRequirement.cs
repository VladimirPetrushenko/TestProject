using Microsoft.AspNetCore.Authorization;

namespace MyApi.ApiKey.Authorization
{
    public class OnlyThirdPartiesRequirement : IAuthorizationRequirement
    {
    }
}
