using CanonFileAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CanonFileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService filesService;

        public FilesController(IFileService filesService)
        {
            this.filesService = filesService;
        }

        // GET: api/<FilesController>
        [HttpGet]
        public IEnumerable<IFile> Get()
        {
            return this.filesService.GetFiles();
        }

        // POST api/<FilesController>
        [HttpPost]
        public IActionResult Post([FromBody] Models.File file)
        {
            if (file == null || String.IsNullOrEmpty(file.Name))
            {
                return BadRequest("File or its properties are missing");
            }

            try
            {
                this.filesService.AddFile(file);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return this.Ok();
        }
    }
}
