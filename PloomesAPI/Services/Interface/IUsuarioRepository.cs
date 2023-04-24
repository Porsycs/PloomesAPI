using PloomesAPI.Model;

namespace PloomesAPI.Services.Interface
{
	public interface IUsuarioRepository
	{
		Usuario ValidaUsuario(Usuario usuario);

		Usuario RefreshUsuario(Usuario usuario);

		Usuario ValidateCredentials(string login);

		bool RevokeToken(string usuarioLogin);
	}
}
