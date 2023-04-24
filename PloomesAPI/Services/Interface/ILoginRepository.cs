using PloomesAPI.Model;

namespace PloomesAPI.Services.Interface
{
	public interface ILoginRepository
	{
		Token ValidateCredentials(Usuario usuario);

		Token ValidateCredentials(Token token);

		bool RevokeToken(string usuarioLogin);
	}
}
