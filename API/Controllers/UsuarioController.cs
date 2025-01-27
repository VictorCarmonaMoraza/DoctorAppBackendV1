using Data.DBContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace API.Controllers
{
    [Route("api/[controller]")]  //api/usuario
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene toda la lista de usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet]  //api/usuario
        public ActionResult<IEnumerable<Usuario>> GetUsuarios()
        {
            var usuarios = _context.Users.ToList();
            return Ok(usuarios);
        }

        /// <summary>
        /// Obtinee un usuario por su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")] //api/usuario
        public ActionResult<Usuario> GetUsuario(int id)
        {
            var usuario = _context.Users.FirstOrDefault(x => x.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }
    }
}
