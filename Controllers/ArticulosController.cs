using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_final_BD.Data;
using Proyecto_final_BD.Models;

namespace Proyecto_final_BD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly InventarioContext _context;

        public ArticulosController(InventarioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Articulo>>> GetArticulos()
        {
            return await _context.Articulos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Articulo>> GetArticulo(int id)
        {
            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo == null) return NotFound();
            return articulo;
        }

        [HttpPost]
        public async Task<ActionResult> CrearArticulo([FromBody] Articulo articulo)
        {
            _context.Articulos.Add(articulo);
            await _context.SaveChangesAsync();
            return Ok("Artículo agregado correctamente");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditarArticulo(int id, [FromBody] Articulo articulo)
        {
            var a = await _context.Articulos.FindAsync(id);
            if (a == null) return NotFound();

            a.Nombre = articulo.Nombre;
            a.Stock = articulo.Stock;
            a.Imagen = articulo.Imagen;

            await _context.SaveChangesAsync();
            return Ok("Artículo actualizado correctamente");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> BorrarArticulo(int id)
        {
            var a = await _context.Articulos.FindAsync(id);
            if (a == null) return NotFound();

            _context.Articulos.Remove(a);
            await _context.SaveChangesAsync();
            return Ok("Artículo eliminado correctamente");
        }
    }
}