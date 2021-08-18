using Microsoft.AspNetCore.Mvc;
using SyCHC.Context;
using SyCHC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Perfiles_ConsultorController : ControllerBase
    {
        private readonly AppDbContext context;
        public Perfiles_ConsultorController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<Perfiles_ConsultorController>
        [HttpGet]
        public IEnumerable<Perfiles_Consultor> Get()
        {
            return context.Perfiles_Consultor.ToList();
        }

        // GET api/<Perfiles_ConsultorController>lista/abc-defg
        [HttpGet("lista-perfiles/{idConsultor}")]
        public ActionResult Get(Guid idConsultor)
        {
            var listaPerfiles = context.Lista_Perfiles_Por_Consultor.Where(lista => lista.IdConsultor == idConsultor);
            if (listaPerfiles != null)
            {
                return Ok(listaPerfiles);
            }
            else
            {
                return NotFound("Perfil del consultor no encontrado.");
            }
        }

        // POST api/<Perfiles_ConsultorController>
        [HttpPost]
        public ActionResult Post([FromBody] Perfiles_Consultor perfiles_Consultor)
        {
            //Verifica que no se duplique
            var existePerfilConsultor = context
                .Perfiles_Consultor
                .FirstOrDefault(
                    pc => 
                    pc.IdConsultor == perfiles_Consultor.IdConsultor &&
                    pc.IdPerfil == perfiles_Consultor.IdPerfil
                );

            if (existePerfilConsultor != null)
            {
                return BadRequest("Este perfil ya está asignado a este consultor.");
            }

            try
            {
                context.Perfiles_Consultor.Add(perfiles_Consultor);
                context.SaveChanges();

                return Ok(perfiles_Consultor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<Perfiles_ConsultorController>/5
        [HttpPut("{id}")]
        public ActionResult Put(long id, [FromBody] Perfiles_Consultor perfiles_Consultor)
        {
            var perfilesConsultorRegistro = context.Perfiles_Consultor.Find(id);
            if (perfilesConsultorRegistro != null)
            {
                try
                {
                    perfilesConsultorRegistro.IdConsultor = perfiles_Consultor.IdConsultor;
                    perfilesConsultorRegistro.IdPerfil = perfiles_Consultor.IdPerfil;
                    perfilesConsultorRegistro.AniosExperiencia = perfiles_Consultor.AniosExperiencia;

                    context.SaveChanges();

                    return Ok(perfiles_Consultor);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                    throw;
                }
            }
            else
            {
                return NotFound("Perfil del consultor no encontrado.");
            }
        }

        // DELETE api/<Perfiles_ConsultorController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            var perfilConsultor = context.Perfiles_Consultor.Find(id);
            if (perfilConsultor != null)
            {
                try
                {
                    context.Perfiles_Consultor.Remove(perfilConsultor);

                    context.SaveChanges();

                    return Ok("Perfil eliminado del consultor con éxito.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                    throw;
                }
            }
            else
            {
                return NotFound("Perfil del consultor no encontrado.");
            }
        }
    }
}
