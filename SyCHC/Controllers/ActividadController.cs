using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyCHC.Context;
using SyCHC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadController : ControllerBase
    {
        private readonly AppDbContext context;
        private IWebHostEnvironment webHostEnvironment;
        public ActividadController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: api/<ActividadController>
        [HttpGet]
        public IEnumerable<Actividad> Get()
        {
            return context.Actividad.ToList();
        }

        // GET api/<ActividadController>/5
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var actividad = context.Actividad.Find(id);
            if (actividad != null)
            {
                return Ok(actividad);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<ActividadController>
        [HttpPost]
        public string Post(/*[FromBody] Actividad actividad,*/ IFormFile archivo)
        {
            string nuevoNombre = "";
            string path = "";

            //Obtiene información del archivo
            if (archivo != null)
            {
                FileInfo fi = new FileInfo(archivo.FileName);
                nuevoNombre = $"File_{DateTime.Now.TimeOfDay.Milliseconds}_{Guid.NewGuid()}_{fi}";
                path = Path.Combine("", webHostEnvironment.ContentRootPath + "/Files/" + nuevoNombre);

                //Guarda archivo en las carpetas del servidor
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    archivo.CopyTo(stream);
                }
            }


            return nuevoNombre;
        }

        // PUT api/<ActividadController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ActividadController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
