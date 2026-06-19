using Microsoft.AspNetCore.Mvc;
using APIestudiantes.Modelos;

namespace APIestudiantes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private static List<EstudianteModel> estudiantes = new List<EstudianteModel>()
        {
            new EstudianteModel()
            {
                Id = 1,
                Nombre = "Juan",
                Apellido = "Pérez",
                Correo = "juan.perez@universidad.com",
                Carrera = "Ingeniería",
                Edad = 20,
                Promedio = 88.5m,
                Activo = true
            },
            new EstudianteModel()
            {
                Id = 2,
                Nombre = "María",
                Apellido = "Gómez",
                Correo = "maria.gomez@universidad.com",
                Carrera = "Medicina",
                Edad = 22,
                Promedio = 96.2m,
                Activo = true
            },
            new EstudianteModel()
            {
                Id = 3,
                Nombre = "Carlos",
                Apellido = "López",
                Correo = "carlos.lopez@universidad.com",
                Carrera = "Ingeniería",
                Edad = 21,
                Promedio = 85.3m,
                Activo = false
            },
            new EstudianteModel()
            {
                Id = 4,
                Nombre = "Ana",
                Apellido = "Martínez",
                Correo = "ana.martinez@universidad.com",
                Carrera = "Arquitectura",
                Edad = 23,
                Promedio = 79.7m,
                Activo = true
            },
            new EstudianteModel()
            {
                Id = 5,
                Nombre = "Luis",
                Apellido = "García",
                Correo = "luis.garcia@universidad.com",
                Carrera = "Medicina",
                Edad = 24,
                Promedio = 68.5m,
                Activo = true
            }
        };
        [HttpGet("ObtenerEstudiantes")]
        public ActionResult<List<EstudianteModel>> ObtenerEstudiantes()
        {
            return Ok(estudiantes);
        }
        [HttpGet("ObtenerEstudiante/{id}")]
        public ActionResult<EstudianteModel> ObtenerEstudiante(int id)
        {
            var estudiante = estudiantes.FirstOrDefault(e => e.Id == id);
            if (estudiante == null)
            {
                return NotFound();
            }
            return Ok(estudiante);
        }
        [HttpPost("AgregarEstudiante")]
        public ActionResult<EstudianteModel> AgregarEstudiante([FromBody] EstudianteModel estudiante)
        {
            estudiante.Id = estudiantes.Count + 1;
            estudiantes.Add(estudiante);
            return CreatedAtAction(nameof(ObtenerEstudiantes), new { id = estudiante.Id }, estudiante);
        }
        [HttpPut("ActualizarDatosEstudiante/{id}")]
        public ActionResult<EstudianteModel> ActualizarDatosEstudiante(int id, [FromBody] EstudianteModel estudianteActualizado)
        {
            var estudiante = estudiantes.FirstOrDefault(e => e.Id == id);
            if (estudiante == null)
            {
                return NotFound();
            }
            estudiante.Nombre = estudianteActualizado.Nombre;
            estudiante.Apellido = estudianteActualizado.Apellido;
            estudiante.Correo = estudianteActualizado.Correo;
            estudiante.Carrera = estudianteActualizado.Carrera;
            estudiante.Edad = estudianteActualizado.Edad;
            estudiante.Promedio = estudianteActualizado.Promedio;
            estudiante.Activo = estudianteActualizado.Activo;
            return NoContent();
        }
        [HttpGet("BuscarEstudiante")]
        public ActionResult<List<EstudianteModel>> BuscarEstudiante([FromQuery] string texto)
        {
            var estudiantesEncontrados = estudiantes.Where(e => e.Nombre.ToLower().Contains(texto.ToLower()) || e.Apellido.ToLower().Contains(texto.ToLower())).ToList();
            if (estudiantesEncontrados.Count == 0)
            {
                return NotFound();
            }
            return Ok(estudiantesEncontrados);
        }
        [HttpGet("ObtenerEstudiantesPorCarrera/{carrera}")]
        public ActionResult<List<EstudianteModel>> ObtenerEstudiantesPorCarrera(string carrera)
        {
            var estudiantesPorCarrera = estudiantes.Where(e => e.Carrera.ToLower() == carrera.ToLower()).ToList();
            if (estudiantesPorCarrera.Count == 0)
            {
                return NotFound();
            }
            return Ok(estudiantesPorCarrera);
        }
        [HttpGet("ObtenerEstudiantesAprobados")]
        public ActionResult<List<EstudianteModel>> ObtenerEstudiantesAprobados()
        {
            var estudiantesAprobados = estudiantes.Where(e => e.Promedio >= 70).ToList();
            if (estudiantesAprobados.Count == 0)
            {
                return NotFound();
            }
            return Ok(estudiantesAprobados);
        }
        [HttpGet("OrdenarEstudiantes")]
        public ActionResult<List<EstudianteModel>> OrdenarEstudiantes([FromQuery] string campo, [FromQuery] string orden)
        {
            if (orden == null || (orden.ToLower() != "asc" && orden.ToLower() != "desc"))
            {
                return BadRequest("El parámetro 'orden' solo puede ser 'asc' o 'desc'.");
            }
            List<EstudianteModel> estudiantesOrdenados;
            switch (campo.ToLower())
            {
                case "nombre":
                    estudiantesOrdenados = orden.ToLower() == "asc" ? estudiantes.OrderBy(e => e.Nombre).ToList() : estudiantes.OrderByDescending(e => e.Nombre).ToList();
                    break;
                case "apellido":
                    estudiantesOrdenados = orden.ToLower() == "asc" ? estudiantes.OrderBy(e => e.Apellido).ToList() : estudiantes.OrderByDescending(e => e.Apellido).ToList();
                    break;
                case "promedio":
                    estudiantesOrdenados = orden.ToLower() == "asc" ? estudiantes.OrderBy(e => e.Promedio).ToList() : estudiantes.OrderByDescending(e => e.Promedio).ToList();
                    break;
                default:
                    return BadRequest("Campo de ordenación no válido.");
            }
            return Ok(estudiantesOrdenados);
        }
        [HttpGet("ObtenerEstudiantesActivos")]
        public ActionResult<List<EstudianteModel>> ObtenerEstudiantesActivos()
        {
            var estudiantesActivos = estudiantes.Where(e => e.Activo).ToList();
            if (estudiantesActivos.Count == 0)
            {
                return NotFound();
            }
            return Ok(estudiantesActivos);
        }
        [HttpDelete("EliminarEstudiante/{id}")]
        public ActionResult EliminarEstudiante(int id)
        {
            var estudiante = estudiantes.FirstOrDefault(e => e.Id == id);
            if (estudiante == null)
            {
                return NotFound();
            }
            estudiantes.Remove(estudiante);
            return NoContent();
        }

    }
}