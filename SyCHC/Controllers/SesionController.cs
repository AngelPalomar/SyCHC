using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc;
using SyCHC.Context;
using SyCHC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace SyCHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SesionController : ControllerBase
    {
        private readonly AppDbContext context;
        public SesionController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<SesionController>
        [HttpGet]
        public IEnumerable<Sesion> Get()
        {
            return context.Sesion.ToList();
        }

        // GET api/<SesionController>/5
        [HttpGet("{clave}")]
        public ActionResult Get(Guid clave)
        {
            var sesion = context.Sesion.Find(clave);
            if (sesion != null)
            {
                return Ok(sesion);
            }
            else
            {
                return NotFound("No existe la sesión.");
            }
        }

        //Método para iniciar sesión
        // POST api/<SesionController>/iniciar
        [HttpPost]
        public ActionResult Post([FromBody] Credencial credencial)
        {
            string contrasenaEncriptada = "";
            SHA512 sha512 = new SHA512Managed();
            var data = Encoding.UTF8.GetBytes(credencial.Contrasena);
            var usuario = context.Usuario.FirstOrDefault(us => us.CorreoElectronico == credencial.CorreoElectronico);

            if (usuario != null)
            {
                //Verifica el estado del usuario
                if (!usuario.Estado)
                {
                    return BadRequest("Usuario no disponible.");
                }

                //Encriptar contraseña
                byte[] hash = sha512.ComputeHash(data);
                contrasenaEncriptada = Convert.ToBase64String(hash);

                //Validar contraseña
                if (usuario.Contrasena == contrasenaEncriptada)
                {
                    try
                    {
                        //Crea la sesión
                        Sesion sesion = new Sesion();

                        sesion.IdUsuario = usuario.Id;
                        sesion.DireccionIP = credencial.DireccionIP;
                        sesion.Fecha = DateTime.Now;
                        usuario.UltimoAcceso = DateTime.Now;

                        context.Sesion.Add(sesion);
                        context.SaveChanges();

                        return Ok(sesion);
                    }
                    catch (Exception)
                    {
                        return BadRequest("No se pudo iniciar la sesión.");
                    }
                }
                else
                {
                    return BadRequest("Usuario o contraseña incorrectos.");
                }
            }
            else
            {
                return NotFound("Este usuario no existe.");
            }
        }

        // DELETE api/<SesionController>/5
        [HttpDelete("{clave}")]
        public ActionResult Delete(Guid clave)
        {
            var sesion = context.Sesion.Find(clave);
            if (sesion != null)
            {
                try
                {
                    context.Sesion.Remove(sesion);
                    context.SaveChanges();

                    return Ok("Sesión terminada correctamente.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound("Sesión no encontrada.");
            }
        }
    }
}
