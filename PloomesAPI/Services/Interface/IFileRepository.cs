using PloomesAPI.Model.ViewModel;

namespace PloomesAPI.Services.Interface
{
	public interface IFileRepository
	{
		byte[] GetArquivo(string nome);
		Task<FileViewModel> SalvarArquivo (IFormFile file);
		Task<List<FileViewModel>> SalvarArquivosMultiplos (IList<IFormFile> files); 
	}
}
