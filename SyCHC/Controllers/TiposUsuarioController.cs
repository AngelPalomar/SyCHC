using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SyCHC.Models;
using SyCHC.Context;

namespace SyCHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposUsuarioController : ControllerBase
    {
        private readonly AppDbContext context;
        public TiposUsuarioController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<TiposUsuarioController>
        [HttpGet]
        public IEnumerable<TiposUsuario> Get()
        {
            return context.TiposUsuario.ToList();
        }

        // GET api/<TiposUsuarioController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var tiposUsuario = context.TiposUsuario.FirstOrDefault(tipus => tipus.Id == id);
            if (tiposUsuario != null)
            {
                return Ok(tiposUsuario);
            } else
            {
                return NotFound();
            }
        }

        // POST api/<TiposUsuarioController>
        [HttpPost]
        public ActionResult Post([FromBody] TiposUsuario tipoUsuario)
        {
            try
            {
                context.TiposUsuario.Add(tipoUsuario);
                context.SaveChanges();

                return Ok(tipoUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<TiposUsuarioController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TiposUsuario tipoUsuario)
        {
            var tiposRegistro = context.TiposUsuario.Find(id);
            if (tiposRegistro != null)
            {
                try
                {
                    tiposRegistro.Tipo = tipoUsuario.Tipo.Trim().ToLower();
                    context.SaveChanges();

                    return Ok(tiposRegistro);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE api/<TiposUsuarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var tp = context.TiposUsuario.Find(id);
            if (tp != null)
            {
                try
                {
                    context.TiposUsuario.Remove(tp);
                    context.SaveChanges();

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            } else
            {
                return NotFound();
            }
        }
    }
}
