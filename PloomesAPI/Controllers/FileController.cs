using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PloomesAPI.Model.ViewModel;
using PloomesAPI.Services.Interface;

namespace PloomesAPI.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	[Authorize("Bearer")]
	public class FileController : ControllerBase
	{
		private readonly IFileRepository _fileRepository;

		public FileController(IFileRepository fileRepository)
		{
			_fileRepository = fileRepository;
		}

		[HttpPost("UploadArquivo")]
		[ProducesResponseType(200, Type = typeof(FileViewModel))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		[Produces("application/json")]
		public async Task<IActionResult> UploadArquivo(IFormFile file)
		{
			FileViewModel detail =	await _fileRepository.SalvarArquivo(file);

			return new OkObjectResult(detail);
		}


		[HttpPost("UploadArquivosMultiplos")]
		[ProducesResponseType(200, Type = typeof(FileViewModel))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		[Produces("application/json")]
		public async Task<IActionResult> UploadArquivosMultiplos(List<IFormFile> files)
		{
			List<FileViewModel> details = await _fileRepository.SalvarArquivosMultiplos(files);

			return new OkObjectResult(details);
		}

		[HttpGet("downloadArquivo/{Nome}")]
		[ProducesResponseType(200, Type = typeof(byte[]))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		[Produces("application/octet-stream")]
		public async Task<IActionResult> DownloadArquivo(string Nome)
		{
			byte[] buffer = _fileRepository.GetArquivo(Nome);

			if (buffer != null)
			{
				HttpContext.Response.ContentType = $"application/{Path.GetExtension(Nome).Replace(".", "")}";
				HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
				await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
			}
				return new ContentResult();
		}
	}
}
