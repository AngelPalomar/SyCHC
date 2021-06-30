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
        [HttpGet("{idUsuario}")]
        public ActionResult Get(Guid idUsuario)
        {
            var sesion = context.Sesion.Where(s => s.IdUsuario == idUsuario);
            if (sesion.ToList().Count > 0)
            {
                return Ok(sesion);
            }
            else
            {
                return NotFound("No hay sesiones existentes.");
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

                        context.Sesion.Add(sesion);
                        context.SaveChanges();

                        return Ok(sesion);
                    }
                    catch (Exception ex)
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
