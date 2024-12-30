using ClientesAPI.Data;
using ClientesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.ClienteID }, cliente);
        }

        // GET: api/Clientes/paginado
        [HttpGet("paginado")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientesPaginado(int page = 1, int pageSize = 10)
        {
            // Cálculo de la paginación
            var clientes = await _context.Clientes
                .Skip((page - 1) * pageSize)  // Saltar los primeros registros de acuerdo a la página solicitada
                .Take(pageSize)  // Tomar solo la cantidad de registros indicada en pageSize
                .ToListAsync();

            return clientes;
        }
    }
}
