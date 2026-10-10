using mi_primer_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace mi_primer_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : Controller
    {
        private readonly DbEmpresaContext _context;
        public ClientesController(DbEmpresaContext ctx)
        {
            _context = ctx;
        }

        [HttpGet("ListarUsuarios")]
        public ActionResult<List<Cliente>> ListarUsuarios()
        {
            return _context.Clientes.ToList();
        }

        [HttpGet("ListarUsuario/{id}")]
        public ActionResult<Cliente> ObtenerrUsuario(int id)
        {
            return _context.Clientes.FirstOrDefault(e => e.IdCliente == id);
        }

        [HttpPost("CrearUsuario")]
        public ActionResult CrearUsuario([FromBody] Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return Ok(new
            {
                mensaje = "Cliente registrado correctamente",
                datos = cliente
            });
        }

        [HttpPut("ActualizarUsuario/{id}")]
        public ActionResult ActualizarUsuario(
            int id, [FromBody] Cliente cliente)
        {
            var existente = _context.Clientes
                .FirstOrDefault(e => e.IdCliente == id);

            if (existente == null)
                return NotFound("Cliente no encontrado");

            _context.Entry(existente).CurrentValues.SetValues(cliente);
            existente.IdCliente = id;

            _context.SaveChanges();

            return Ok("Cliente actualizado correctamente");
        }

        [HttpDelete("EliminarUsuario/{id}")]
        public ActionResult EliminarUsuario(int id)
        {
            var cliente = _context.Clientes
                .FirstOrDefault(e => e.IdCliente == id);

            if (cliente == null)
                return NotFound("Cliente no encontrado");

            _context.Clientes.Remove(cliente);
            _context.SaveChanges();

            return Ok("Cliente eliminado correctamente");
        }

    }
}