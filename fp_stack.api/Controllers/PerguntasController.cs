using fp_stack.core.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fp_stack.api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{apiVersion}/[controller]")]
    [EnableCors("Default")]
    public class PerguntasController : Controller
    {
        private readonly Context _context;

        public PerguntasController(Context context)
        {
            _context = context;
        }

        [HttpGet]        
        public ActionResult<List<Pergunta>> Get()
        {
            var perguntas = _context.Perguntas.ToList();
            //if (perguntas.Count() == 3)
            //    return BadRequest();
            return perguntas;
        }
        
        [HttpPost]
        public ActionResult Post(Pergunta pergunta)
        {
            if (ModelState.IsValid)
            {
                _context.Perguntas.Add(pergunta);
                _context.SaveChanges();
                return Created($"/api/perguntas/{pergunta.Id}", pergunta);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public ActionResult Put(int id, [FromBody]Pergunta pergunta)
        {
            if (ModelState.IsValid)
            {
                //_context.Attach(pergunta);
                _context.Entry(pergunta).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return Created($"/api/perguntas/{pergunta.Id}", pergunta);
            }

            return BadRequest(ModelState);
        }
    }
}
