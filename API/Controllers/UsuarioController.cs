using Data.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace API.Controllers
{

    public class UsuarioController : BaseApiController
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
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios =await  _context.Users.ToListAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Obtinee un usuario por su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")] //api/usuario
        public async  Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }
    }
}
