using PloomesAPI.Common;

namespace PloomesAPI.Services.Interface
{
	public interface IClienteRepository
	{
		Cliente GetClienteByNome(string nome);
	}
}
