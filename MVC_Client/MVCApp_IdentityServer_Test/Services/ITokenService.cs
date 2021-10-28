using System.Threading.Tasks;
using IdentityModel.Client;

namespace MVCApp_IdentityServer_Test.Services
{
  public interface ITokenService
  {
    Task<TokenResponse> GetToken(string scope);
  }
}