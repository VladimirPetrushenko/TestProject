using System.Threading.Tasks;

namespace MyApi.Configuration.Authentication
{
    public interface IGetApiKeyQuery
    {
        Task<ApiKey> Execute(string providedApiKey);
    }
}
