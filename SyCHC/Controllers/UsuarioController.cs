using Microsoft.AspNetCore.Mvc;
using SyCHC.Context;
using SyCHC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace SyCHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //Invoca el contexto de la base de datos
        private readonly AppDbContext context;
        public UsuarioController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<UsuarioController>
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            return context.Usuario.ToList();
        }

        // GET api/<UsuarioController>/5
        [HttpGet("id/{id}")]
        public ActionResult Get(Guid id)
        {
            var usuario = context.Usuario.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
            {
                return Ok(usuario);
            } else
            {
                return NotFound("Usuario no encontrado.");
            }
        }

        [HttpGet("email/{correoElectronico}")]
        public ActionResult Get(string correoElectronico)
        {
            var usuario = context.Usuario.FirstOrDefault(u => u.CorreoElectronico == correoElectronico);
            if (usuario != null)
            {
                return Ok(usuario);
            }
            else
            {
                return NotFound("Usuario no encontrado.");
            }
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public ActionResult Post([FromBody] Usuario usuario)
        {
            Usuario usuarioNuevo = new Usuario();
            var data = Encoding.UTF8.GetBytes(usuario.Contrasena);
            SHA512 sha512 = new SHA512Managed();

            try
            {
                //Encriptar contraseña
                byte[] hash = sha512.ComputeHash(data);

                //Crear los datos del usuario
                usuarioNuevo.Contrasena = Convert.ToBase64String(hash);
                usuarioNuevo.CorreoElectronico = usuario.CorreoElectronico;
                usuarioNuevo.Estado = usuario.Estado;
                usuarioNuevo.FechaRegistro = usuario.FechaRegistro;
                usuarioNuevo.TipoUsuario = usuario.TipoUsuario;

                //Añade el usuario
                context.Usuario.Add(usuarioNuevo);
                context.SaveChanges();

                return Ok(usuarioNuevo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Usuario usuario)
        {
            SHA512 sha512 = new SHA512Managed();
            var data = Encoding.UTF8.GetBytes(usuario.Contrasena);
            var usuarioRegistro = context.Usuario.Find(id);

            if (usuarioRegistro != null)
            {
                try
                {
                    //Encriptar contraseña
                    byte[] hash = sha512.ComputeHash(data);

                    //Crear los datos del usuario
                    usuarioRegistro.Contrasena = Convert.ToBase64String(hash);
                    usuarioRegistro.CorreoElectronico = usuario.CorreoElectronico;
                    usuarioRegistro.Estado = usuario.Estado;
                    usuarioRegistro.FechaRegistro = usuario.FechaRegistro;
                    usuarioRegistro.TipoUsuario = usuario.TipoUsuario;

                    context.SaveChanges();

                    return Ok(usuarioRegistro);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound("Usuario no encontrado.");
            }
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var usuario = context.Usuario.Find(id);
            if (usuario != null)
            {
                try
                {
                    context.Usuario.Remove(usuario);
                    context.SaveChanges();

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            } else
            {
                return NotFound("Usuario no encontrado.");
            }
        }
    }
}
