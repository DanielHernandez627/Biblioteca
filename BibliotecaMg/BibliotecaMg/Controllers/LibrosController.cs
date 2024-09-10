using BibliotecaMg.Models;
using BibliotecaMg.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrosController : ControllerBase
    {

        private readonly LibroService _libroService;

        public LibrosController(LibroService libroService)
        {
            _libroService = libroService;
        }

        [HttpGet]
        public async Task<List<Libro>> Get() =>
            await _libroService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Libro>> Get(string id)
        {
            var libro = await _libroService.GetAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            return libro;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Libro newLibro)
        {
            await _libroService.CreateAsync(newLibro);
            return CreatedAtAction(nameof(Get), new { id = newLibro.Id }, newLibro);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Libro updatedLibro)
        {
            var libro = await _libroService.GetAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            updatedLibro.Id = libro.Id;

            await _libroService.UpdateAsync(id, updatedLibro);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var libro = await _libroService.GetAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            await _libroService.RemoveAsync(id);

            return NoContent();
        }

        [HttpPatch("{id:length(24)}/estado")]
        public async Task<IActionResult> UpdateEstado(string id, [FromBody] bool nuevoEstado)
        {
            var libro = await _libroService.GetAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            await _libroService.UpdateEstadoAsync(id, nuevoEstado);

            return NoContent();
        }

        [HttpGet("nuevoId")]
        public async Task<ActionResult<int>> GetNuevoId()
        {
            int libroid = await _libroService.GetIdLibroAsync();

            if (libroid == 0)
            {
                return BadRequest(libroid);
            }

            return Ok(libroid);
        }
    }
}
