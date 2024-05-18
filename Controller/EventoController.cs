using Microsoft.AspNetCore.Mvc;
using EventosServicio.Models;
using Microsoft.EntityFrameworkCore;
namespace EventosServicio.Controller

{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {   
        private readonly pruebaTecnicaDBContext _dbcontext;
        public EventoController(pruebaTecnicaDBContext context)
        {
            _dbcontext = context; 
        }
        //Método para listar todos los Eventos
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            List<Evento> lista = await _dbcontext.Eventos.
                Where(e => e.Eliminado == false).
                OrderByDescending(c => c.Id).
                ToListAsync();
            return StatusCode(StatusCodes.Status200OK, lista);
        }
        //Metodo para listar los elementos y consultar con paginacion
        [HttpGet]
        [Route("ListaP")]
        public async Task<IActionResult> ListaP(int numeroPagina = 1, int tamanioPagina = 10)
        {
            int registrosTotales = await _dbcontext.Eventos.CountAsync(e => e.Eliminado == false);
            int paginasTotales = (int)Math.Ceiling((double)registrosTotales / tamanioPagina);
            List<Evento> lista = await _dbcontext.Eventos.
                Where(e => e.Eliminado == false).
                OrderByDescending(c => c.Id).
                Skip((numeroPagina - 1) * tamanioPagina).
                Take(tamanioPagina).
                ToListAsync();
            var result = new
            {
                RegistrosTotales = registrosTotales,
                PaginasTotales = paginasTotales,
                NumeroPagina = numeroPagina,
                TamanioPagina = tamanioPagina,
                Data = lista
            };
            //return StatusCode(StatusCodes.Status200OK, result);
            return StatusCode(StatusCodes.Status200OK, result);
        }
        //Metodo para obtener los datos de un evento en especifico 
        [HttpGet]
        [Route("Detalles/{id:int}")]
        public async Task<IActionResult> Detalles(int id)
        {
            Evento evento = await _dbcontext.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound("Evento no encontrado.");
            }
            return StatusCode(StatusCodes.Status200OK, evento);
        }
        //Metodo para registrar un evento
        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] Evento request)
        {
            await _dbcontext.Eventos.AddAsync(request);
            await _dbcontext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, "OK");
        }
        //Metodo para editar un evento 
        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Evento request)
        {
            _dbcontext.Eventos.Update(request);
            await _dbcontext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, "OK");
        }
        //Mtodo para la eliminación logica de un evento 
        [HttpPut]
        [Route("EliminarL/{id:int}")]
        public async Task<IActionResult> EliminarLogico(int id)
        {
            Evento evento = await _dbcontext.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            evento.Eliminado = true;
            _dbcontext.Eventos.Update(evento);
            await _dbcontext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, "OK");
        }
        //Metodo para la eliminacion de un evento de DB
        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            Evento evento = _dbcontext.Eventos.Find(id);
            _dbcontext.Eventos.Remove(evento);
            await _dbcontext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, "OK");

        }
        


    }
}
