using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoRepository _repo;

        public ProductosController(IProductoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Producto>> GetAll()
        {
            return Ok(_repo.GetAll());
        }

        [HttpGet("{id:int}")]
        public ActionResult<Producto> GetById(int id)
        {
            var producto = _repo.GetById(id);
            return producto is null ? NotFound() : Ok(producto);
        }

        [HttpPost]
        public ActionResult<Producto> Create(Producto nuevo)
        {
            _repo.Add(nuevo);
            return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Producto actualizado)
        {
            var producto = _repo.GetById(id);
            if (producto is null) return NotFound();

            actualizado.Id = id;
            _repo.Update(actualizado);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var producto = _repo.GetById(id);
            if (producto is null) return NotFound();

            _repo.Delete(id);
            return NoContent();
        }
    }
}