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

        // GET: api/Clientes/id
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
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientesPaginado( int page = 1, int pageSize = 10, string campo_orden = "OrdNombre", string orden = "Ascendente", string? nombre = null, string? pais = null)
        {
            try
            {
                var parameters = new[]
                {
                    new Microsoft.Data.SqlClient.SqlParameter("@PageSize", pageSize),
                    new Microsoft.Data.SqlClient.SqlParameter("@PageIndex", page),
                    new Microsoft.Data.SqlClient.SqlParameter("@campo_orden", campo_orden),
                    new Microsoft.Data.SqlClient.SqlParameter("@orden", orden),
                    new Microsoft.Data.SqlClient.SqlParameter("@Nombre", nombre ?? (object)DBNull.Value),
                    new Microsoft.Data.SqlClient.SqlParameter("@Pais", pais ?? (object)DBNull.Value)
                };

            var clientes = await _context.Clientes
                .FromSqlRaw("EXEC CLIENTES_PAGINACION_PAGINAS @PageSize, @PageIndex, @campo_orden, @orden, @Nombre, @Pais", parameters)
                .ToListAsync();

            return clientes;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al ejecutar el procedimiento almacenado.", details = ex.Message });
            }
        }


     }
}
