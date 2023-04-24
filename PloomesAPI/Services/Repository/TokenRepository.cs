using Microsoft.IdentityModel.Tokens;
using PloomesAPI.Configurations;
using PloomesAPI.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PloomesAPI.Services.Repository
{
	public class TokenRepository : ITokenRepository
	{
		private TokenConfiguration _tokenConfiguration;

		public TokenRepository(TokenConfiguration tokenConfiguration)
		{
			_tokenConfiguration = tokenConfiguration;
		}

		public string GenerateAccessToken(IEnumerable<Claim> claims)
		{
			var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Secret));
			var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

			var tokenOptions = new JwtSecurityToken(
				issuer: _tokenConfiguration.Issuer,
				audience: _tokenConfiguration.Audience,
				claims: claims,
				expires: DateTime.Now.AddMinutes(_tokenConfiguration.Minutes),
				signingCredentials: signinCredentials 
				);

			var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
			return token;
		}

		public string GenerateRefreshToken()
		{
			var randNum = new byte[32];
			using(var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(randNum);
				return Convert.ToBase64String(randNum);
			};
		}

		public ClaimsPrincipal GetPrincipal(string token)
		{
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateAudience = false,
				ValidateIssuer = false,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Secret)),
				ValidateLifetime = false
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			SecurityToken securityToken;

			var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
			var jwtSecurityToken = securityToken as JwtSecurityToken;

			if (jwtSecurityToken == null ||
				!jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture)) 
				throw new SecurityTokenException("Token Invalido");

			return principal;
		}
	}
}
