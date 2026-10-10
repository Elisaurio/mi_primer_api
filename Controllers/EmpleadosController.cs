
using mi_primer_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mi_primer_api.Controllers
{
    public class EmpleadoDatos
    {
        public int IdPuesto { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class EmpleadosController : ControllerBase
    {
        private readonly DbEmpresaContext _context;

        public EmpleadosController(DbEmpresaContext ctx)
        {
            _context = ctx;
        }

        [HttpGet("ListarEmpleados")]
        public ActionResult ListarEmpleados()
        {
            var empleados = _context.Empleados
                .Select(e => new
                {
                    e.IdEmpleado,
                    e.Nombre,
                    e.Apellido,
                    e.IdPuesto,
                    Puesto = e.IdPuestoNavigation.Nombre
                }).ToList();

            return Ok(empleados);
        }

        [HttpGet("ListarEmpleado/{id}")]
        public ActionResult ListarEmpleado(int id)
        {
            var empleado = _context.Empleados
                .Where(e => e.IdEmpleado == id)
                .Select(e => new
                {
                    e.IdEmpleado,
                    e.Nombre,
                    e.Apellido,
                    e.IdPuesto,
                    Puesto = e.IdPuestoNavigation.Nombre
                }).FirstOrDefault();

            if (empleado == null)
                return NotFound("Empleado no encontrado");

            return Ok(empleado);
        }

        [HttpPost("CrearEmpleado")]
        public ActionResult CrearEmpleado(
            [FromBody] EmpleadoDatos datos)
        {
            bool puestoExiste = _context.Puestos
                .Any(p => p.IdPuestos == datos.IdPuesto);

            if (!puestoExiste)
                return BadRequest("El puesto no existe");

            var empleado = new Empleado
            {
                IdPuesto = datos.IdPuesto,
                Nombre = datos.Nombre,
                Apellido = datos.Apellido
            };

            _context.Empleados.Add(empleado);
            _context.SaveChanges();

            return Ok(new
            {
                mensaje = "Empleado registrado correctamente",
                empleado.IdEmpleado
            });
        }

        [HttpPut("ActualizarEmpleado/{id}")]
        public ActionResult ActualizarEmpleado(
            int id, [FromBody] EmpleadoDatos datos)
        {
            var empleado = _context.Empleados
                .FirstOrDefault(e => e.IdEmpleado == id);

            if (empleado == null)
                return NotFound("Empleado no encontrado");

            bool puestoExiste = _context.Puestos
                .Any(p => p.IdPuestos == datos.IdPuesto);

            if (!puestoExiste)
                return BadRequest("El puesto no existe");

            empleado.Nombre = datos.Nombre;
            empleado.Apellido = datos.Apellido;
            empleado.IdPuesto = datos.IdPuesto;

            _context.SaveChanges();

            return Ok("Empleado actualizado correctamente");
        }

        [HttpDelete("EliminarEmpleado/{id}")]
        public ActionResult EliminarEmpleado(int id)
        {
            var empleado = _context.Empleados
                .FirstOrDefault(e => e.IdEmpleado == id);

            if (empleado == null)
                return NotFound("Empleado no encontrado");

            _context.Empleados.Remove(empleado);
            _context.SaveChanges();

            return Ok("Empleado eliminado correctamente");
        }
    }
}
