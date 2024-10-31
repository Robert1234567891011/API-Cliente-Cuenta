using BCP.Data;
using BCP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//autor Richard Robertopoma/github.com/Robert1234567891011
namespace BCP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CuentasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{clienteId}")]
        public async Task<ActionResult<IEnumerable<Cuenta>>> GetCuentas(int clienteId)
        {
            return await _context.Cuentas.Where(c => c.ClienteId == clienteId).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Cuenta>> PostCuenta(Cuenta cuenta)
        {
            if (cuenta == null)
                return BadRequest("Cuenta is null.");

            var clienteExists = await _context.Clientes.AnyAsync(c => c.Id == cuenta.ClienteId);
            if (!clienteExists)
                return BadRequest("El cliente no existe.");

            _context.Cuentas.Add(cuenta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCuentas), new { clienteId = cuenta.ClienteId }, cuenta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuenta(int id, Cuenta cuenta)
        {
            if (id != cuenta.Id)
            {
                return BadRequest("ID de la cuenta no coincide.");
            }

            // Buscar la cuenta existente en la base de datos
            var cuentaExistente = await _context.Cuentas.FindAsync(id);
            if (cuentaExistente == null)
            {
                return NotFound("Cuenta no encontrada.");
            }

            // Verificar que el ClienteId no haya cambiado
            if (cuentaExistente.ClienteId != cuenta.ClienteId)
            {
                return BadRequest("El Cliente ID no puede cambiar.");
            }

            // Actualizar los campos necesarios
            cuentaExistente.TipoProducto = cuenta.TipoProducto;
            cuentaExistente.NumeroCuenta = cuenta.NumeroCuenta;
            cuentaExistente.Moneda = cuenta.Moneda;
            cuentaExistente.Monto = cuenta.Monto;
            cuentaExistente.FechaCreacion = cuenta.FechaCreacion;
            cuentaExistente.Sucursal = cuenta.Sucursal;

            // Guardar los cambios
            await _context.SaveChangesAsync();

            return NoContent(); // Respuesta exitosa sin contenido
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuenta(int id)
        {
            var cuenta = await _context.Cuentas.FindAsync(id);
            if (cuenta == null)
                return NotFound();

            _context.Cuentas.Remove(cuenta);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool CuentaExists(int id) => _context.Cuentas.Any(e => e.Id == id);
    }
}
