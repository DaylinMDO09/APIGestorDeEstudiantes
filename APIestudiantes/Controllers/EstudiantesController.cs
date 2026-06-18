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
        [HttpPost("AgregarEstudiante")]
        public ActionResult<EstudianteModel> AgregarEstudiante(EstudianteModel estudiante)
        {
            estudiante.Id = estudiantes.Count + 1;
            estudiantes.Add(estudiante);
            return CreatedAtAction(nameof(ObtenerEstudiantes), new { id = estudiante.Id }, estudiante);
        }

    }
}
