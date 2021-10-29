using System.Threading.Tasks;

namespace MyApi.ApiKey.Authentication
{
    public interface IGetApiKeyQuery
    {
        Task<ApiKey> Execute(string providedApiKey);
    }
}
