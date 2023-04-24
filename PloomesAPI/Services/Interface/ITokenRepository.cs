using System.Security.Claims;

namespace PloomesAPI.Services.Interface
{
	public interface ITokenRepository
	{
		string GenerateAccessToken(IEnumerable<Claim> claims);
		string GenerateRefreshToken();
		ClaimsPrincipal GetPrincipal(string token);
	}
}
