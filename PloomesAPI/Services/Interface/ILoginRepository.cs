using PloomesAPI.Model;
using PloomesAPI.Model.ViewModel;

namespace PloomesAPI.Services.Interface
{
	public interface ILoginRepository
	{
		Token ValidateCredentials(UsuarioViewModel usuario);

		Token ValidateCredentials(TokenViewModel token);

		bool RevokeToken(string usuarioLogin);
	}
}
