using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivoController : ControllerBase
    {
        private IWebHostEnvironment webHostEnvironment;
        public ArchivoController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        // POST api/archivo
        [HttpPost]
        public ActionResult Post(IFormFile archivo)
        {
            string nuevoNombre = "";
            string path = "";

            //Obtiene información del archivo
            if (archivo != null)
            {
                try
                {
                    FileInfo fi = new FileInfo(archivo.FileName);
                    nuevoNombre = $"File_{DateTime.Now.TimeOfDay.Milliseconds}_{Guid.NewGuid()}_{fi}";
                    path = Path.Combine("", webHostEnvironment.ContentRootPath + "/Files/" + nuevoNombre);

                    //Guarda archivo en las carpetas del servidor
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        archivo.CopyTo(stream);
                    }

                    return Ok(nuevoNombre);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Un archivo es requerido.");
            }
        }

        //Método para mostrar una imagen o descargar archivo
        [HttpGet("{fileName}")]
        public IActionResult Get(string fileName)
        {
            var filePath = $"Files/{fileName}";

            try
            {
                byte[] bytes = System.IO.File.ReadAllBytes(filePath);

                new FileExtensionContentTypeProvider().TryGetContentType(fileName, out string contentType);

                return File(bytes, contentType);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
