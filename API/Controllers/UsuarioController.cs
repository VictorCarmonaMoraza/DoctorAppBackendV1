using Data.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entities;
using System.Security.Cryptography;
using System.Text;

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
            var usuarios = await _context.Users.ToListAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Obtinee un usuario por su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")] //api/usuario
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        /// <summary>
        /// Registra un usuario en la base de datos
        /// </summary>
        /// <param name="registroDto"></param>
        /// <returns></returns>
        [HttpPost("registro")]
        public async Task<ActionResult<Usuario>> Registro(RegistroDto registroDto)
        {
            if (await UsuarioExiste(registroDto.Username)) return BadRequest("El usuario ya esta registrado en la base de datos");

            using var hmac = new HMACSHA512();
            var usuario = new Usuario
            {
                Username = registroDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registroDto.Pasword)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        /// <summary>
        /// Valida si un usuario existe en la base de datos
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private async Task<bool> UsuarioExiste(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username.ToLower());
        }
    }
}
