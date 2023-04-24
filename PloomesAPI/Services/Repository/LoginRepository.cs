using PloomesAPI.Configurations;
using PloomesAPI.Model;
using PloomesAPI.Services.Interface;
using PloomesAPI.Services.Interface.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PloomesAPI.Services.Repository
{
	public class LoginRepository : ILoginRepository
	{
		private const string dateFormat = "yyyy-MM-dd HH:mm:ss";
		private TokenConfiguration _tokenConfiguration;
		private IUsuarioRepository _usuarioRepository;
		private readonly ITokenRepository _tokenRepository;

		public LoginRepository(TokenConfiguration tokenConfiguration, IUsuarioRepository usuarioRepository, ITokenRepository tokenRepository)
		{
			_tokenConfiguration = tokenConfiguration;
			_usuarioRepository = usuarioRepository;
			_tokenRepository = tokenRepository;

		}

		public Token ValidateCredentials(Usuario usuarioAcesso)
		{
			var usuario = _usuarioRepository.ValidaUsuario(usuarioAcesso);

			if (usuario == null)
				return null;

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
				new Claim(JwtRegisteredClaimNames.UniqueName, usuario.UsuarioLogin)
			};

			var accessToken = _tokenRepository.GenerateAccessToken(claims);
			var refreshToken = _tokenRepository.GenerateRefreshToken();

			usuario.RefreshToken = refreshToken;
			usuario.ExpireTokenTime = DateTime.Now.AddDays(_tokenConfiguration.DaysToExpiry);
			_usuarioRepository.RefreshUsuario(usuario);

			DateTime createDate = DateTime.Now;
			DateTime expirationDate = createDate.AddMinutes(_tokenConfiguration.Minutes);


			return new Token
				(
				 true,
				 createDate.ToString(dateFormat),
				 expirationDate.ToString(dateFormat),
				 accessToken,
				 refreshToken
				);
		}

		public Token ValidateCredentials(Token token)
		{
			var accessToken = token.AccessToken;
			var refreshToken = token.RefreshToken;

			var principal = _tokenRepository.GetPrincipal(accessToken);

			var usuario = principal.Identity?.Name;

			var user = _usuarioRepository.ValidateCredentials(usuario);

			if (user == null || user.RefreshToken != refreshToken || user.ExpireTokenTime <= DateTime.Now)
				return null;

			accessToken = _tokenRepository.GenerateAccessToken(principal.Claims);
			refreshToken = _tokenRepository.GenerateRefreshToken();

			user.RefreshToken = refreshToken;
			_usuarioRepository.RefreshUsuario(user);

			DateTime createDate = DateTime.Now;
			DateTime expirationDate = createDate.AddMinutes(_tokenConfiguration.Minutes);


			return new Token
				(
				 true,
				 createDate.ToString(dateFormat),
				 expirationDate.ToString(dateFormat),
				 accessToken,
				 refreshToken
				);

		}

		public bool RevokeToken(string usuarioLogin)
		{
			return _usuarioRepository.RevokeToken(usuarioLogin);
		}
	}
}
