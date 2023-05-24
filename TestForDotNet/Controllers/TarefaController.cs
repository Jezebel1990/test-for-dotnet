using Microsoft.AspNetCore.Mvc;
using TestForDotNet.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestForDotNet.Controllers
{
    [Route("api/[controller]")]

    public class TarefaController : Controller
    {
        private readonly ITarefaRepositorio _tarefaRepositorio;

        public TarefaController(ITarefaRepositorio tarefaRepositorio)
        {
            _tarefaRepositorio = tarefaRepositorio;
        }

        // GET: api/<TarefaController>
        [HttpGet]
        public IEnumerable<TarefaItem> GetAll()
        {
            return _tarefaRepositorio.GetAll();
        }

        // GET api/<TarefaController>/5
        [HttpGet("{id}", Name = "GetTarefa")]
        public IActionResult GetById(int id)
        {
            var item = _tarefaRepositorio.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }


        // POST api/<TarefaController>
        [HttpPost]
        public IActionResult Create([FromBody] TarefaItem item)
        {
            if (item == null)
            { 
                return BadRequest();
            }
            _tarefaRepositorio.Add(item);
            return CreatedAtRoute("GetTarefa", new { id = item.Id },item);
        }

        // PUT api/<TarefaController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TarefaItem item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }
            var tarefa = _tarefaRepositorio.Find(id);
            if (tarefa == null) 
            {
                return NotFound();
            }
            tarefa.EstaCompleta = item.EstaCompleta;
            tarefa.Name = item.Name;

            _tarefaRepositorio.Update(tarefa);
            return new NoContentResult();

        }

        // DELETE api/<TarefaController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _tarefaRepositorio.Find(id);
            if (todo == null)
            {
                return NotFound();
            }
            _tarefaRepositorio.Remove(id);
            return new NoContentResult();
        }
    }
}
