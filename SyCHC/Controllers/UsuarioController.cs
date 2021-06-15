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
        public Usuario Get(Guid id)
        {
            var usuario = context.Usuario.FirstOrDefault(u => u.Id == id);
            return usuario;
        }

        [HttpGet("email/{correoElectronico}")]
        public Usuario Get(string correoElectronico)
        {
            var usuario = context.Usuario.FirstOrDefault(u => u.CorreoElectronico == correoElectronico);
            return usuario;
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public ActionResult Post([FromBody] Usuario usuario)
        {
            Usuario u = new Usuario();
            var data = Encoding.UTF8.GetBytes(usuario.Contrasena);
            SHA512 sha512 = new SHA512Managed();

            try
            {
                //Encriptar contraseña
                byte[] hash = sha512.ComputeHash(data);

                //Crear los datos del usuario
                u.Contrasena = Convert.ToBase64String(hash);
                u.CorreoElectronico = usuario.CorreoElectronico;
                u.Estado = usuario.Estado;
                u.FechaRegistro = usuario.FechaRegistro;
                u.TipoUsuario = usuario.TipoUsuario;

                //Añade el usuario
                context.Usuario.Add(u);
                context.SaveChanges();

                return Ok(u);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Usuario usuario)
        {
            SHA512 sha512 = new SHA512Managed();
            var data = Encoding.UTF8.GetBytes(usuario.Contrasena);
            var u = context.Usuario.Find(id);

            if (u != null)
            {
                //Encriptar contraseña
                byte[] hash = sha512.ComputeHash(data);

                //Crear los datos del usuario
                u.Contrasena = Convert.ToBase64String(hash);
                u.CorreoElectronico = usuario.CorreoElectronico;
                u.Estado = usuario.Estado;
                u.FechaRegistro = usuario.FechaRegistro;
                u.TipoUsuario = usuario.TipoUsuario;

                context.SaveChanges();
                return Ok(u);
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var usuario = context.Usuario.Find(id);
            if (usuario != null)
            {
                context.Usuario.Remove(usuario);
                context.SaveChanges();

                return Ok(usuario);
            } else
            {
                return NotFound();
            }
        }
    }
}
