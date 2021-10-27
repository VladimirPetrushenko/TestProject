using System.Threading.Tasks;

namespace MyFirstTestProject.Features.Authentication
{
    public interface IGetApiKeyQuery
    {
        Task<ApiKey> Execute(string providedApiKey);
    }
}
