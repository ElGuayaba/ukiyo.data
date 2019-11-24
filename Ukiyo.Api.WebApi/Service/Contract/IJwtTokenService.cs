using System.Threading.Tasks;
using Ukiyo.Api.WebApi.Service.Implementation;

namespace Ukiyo.Api.WebApi.Service.Contract
{
    public interface IJwtTokenService
    {
        Task<JwtToken> Generate(string userId);
    }
}