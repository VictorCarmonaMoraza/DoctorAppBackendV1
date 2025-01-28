using Data.DBContext;
using Data.Interfaces;
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
        private readonly ITokenServicio _tokenServicio;

        public UsuarioController(ApplicationDbContext context, ITokenServicio tokenServicio)
        {
            _context = context;            _tokenServicio = tokenServicio;

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
        public async Task<ActionResult<UsuarioDto>> Registro(RegistroDto registroDto)
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
            return new UsuarioDto
            {
                Username = usuario.Username,
                Token = _tokenServicio.CreatrToken(usuario)
            };
        }

        /// <summary>
        /// Login de Usuario
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("login")]  //POST: api/usuario/login
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            var usuario = await _context.Users.SingleOrDefaultAsync(x => x.Username == loginDto.Username);
            //Si el usuario no esta en la base de datos retornamos un mensaje de error
            if (usuario == null) return Unauthorized("Usuario no valido");

            using var hmac = new HMACSHA512(usuario.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            //Comparamos caracterer por caracter el hash de la contraseña
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != usuario.PasswordHash[i]) return Unauthorized("Contraseña incorrecta");
            }

            return new UsuarioDto
            {
                Username = usuario.Username,
                Token = _tokenServicio.CreatrToken(usuario)
            };
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
