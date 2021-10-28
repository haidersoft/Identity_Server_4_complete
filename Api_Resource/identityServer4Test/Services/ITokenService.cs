using System.Threading.Tasks;
using IdentityModel.Client;

namespace identityServer4Test.Services
{
  public interface ITokenService
  {
    Task<TokenResponse> GetToken(string scope);
  }
}