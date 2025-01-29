using API.Errores;
using Data.DBContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace API.Controllers
{
    public class ErrorTestController: BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public ErrorTestController(ApplicationDbContext context)
        {
            _context=context;
        }

        /// <summary>
        /// Establece mensaje de error de no autorizado
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetNotAuthorize()
        {
            return "no Autorizado";
        }

        /// <summary>
        /// Establece mensaje de error cuando un usuario no existe
        /// </summary>
        /// <returns></returns>
        [HttpGet("not-found")]
        public ActionResult<Usuario> GetNotFound()
        {
            var objeto = _context.Users.Find(-1);
            if (objeto == null) return NotFound(new ApiErrorResponse(404));
            return Ok(objeto);
        }


       /// <summary>
       /// Estavlece mensaje de error del servidor
       /// </summary>
       /// <returns></returns>
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var objeto = _context.Users.Find(-1);
            var objetoString = objeto.ToString();
            return objetoString;
        }

        /// <summary>
        /// Establece mensaje de error de solicitud no valida
        /// </summary>
        /// <returns></returns>
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
          return BadRequest(new ApiErrorResponse(400));
        }
    }
}
