using PloomesAPI.Model.ViewModel;
using PloomesAPI.Services.Interface;
using PloomesAPI.Services.Interface.Generic;

namespace PloomesAPI.Services.Repository
{
	public class FileRepository : IFileRepository
	{
		private readonly string _basePath;
		private readonly IHttpContextAccessor _contextAccessor;

		public FileRepository(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
			_basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\";
		}

		public byte[] GetArquivo(string nome)
		{
			var filePath = _basePath + nome;
			return File.ReadAllBytes(filePath);
		}

		public async Task<FileViewModel> SalvarArquivo(IFormFile file)
		{
			FileViewModel fileViewModel = new FileViewModel();
			if (file != null && file.Length > 0)
			{
				try
				{
					var fileType = Path.GetExtension(file.FileName);
					var baseUrl = _contextAccessor.HttpContext.Request.Host;

					if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" || fileType.ToLower() == ".png" || fileType.ToLower() == ".jpeg")
					{
						var docName = Path.GetFileName(file.FileName);
						var destination = Path.Combine(_basePath, "", docName);

						fileViewModel.DocName = docName;
						fileViewModel.DocType = fileType;
						fileViewModel.DocUrl = Path.Combine(baseUrl + "/api/v1/File/" + fileViewModel.DocName);

						using var stream = new FileStream(destination, FileMode.Create);
						await file.CopyToAsync(stream);
					}
				}
				catch 
				{
					return null;
				}
				return fileViewModel;
			}
			return null;
		}

		public async Task<List<FileViewModel>> SalvarArquivosMultiplos(IList<IFormFile> files)
		{
			List<FileViewModel> list = new List<FileViewModel>();
			try
			{
				foreach (var file in files)
				{
					list.Add(await SalvarArquivo(file));
				}
				return list;
			}
			catch
			{
				return null;
			}
		}

	}
}
