using PloomesAPI.Model;
using PloomesAPI.Model.ViewModel;

namespace PloomesAPI.Services.Interface
{
	public interface IUsuarioRepository
	{
		Usuario ValidaUsuario(UsuarioViewModel usuario);

		Usuario RefreshUsuario(Usuario usuario);

		Usuario ValidateCredentials(string login);

		bool RevokeToken(string usuarioLogin);
	}
}
