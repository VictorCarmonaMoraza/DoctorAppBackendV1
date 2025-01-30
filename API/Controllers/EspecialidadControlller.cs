using BLL.Serivicios.Interfaces;
using Data.Interfaces.IRepositorio;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.Net;

namespace API.Controllers
{
    public class EspecialidadController : BaseApiController
    {

        private readonly IEspecialidadServicio _especialidadServicio;
        private ApiResponse _response;

        public EspecialidadController(IEspecialidadServicio especialidadServicio)
        {
            _especialidadServicio = especialidadServicio;
            _response = new ApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var especialidades = await _especialidadServicio.ObtenerEspecialidades();
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsExitoso = true;
                _response.Mensaje = "Especialidades obtenidas correctamente";
                _response.Resultado = especialidades;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Mensaje = ex.Message;
            }
            return Ok(_response);

        }

        [HttpPost]
        public async Task<IActionResult> Post(EspecialidadDto especialidadDto)
        {
            try
            {
                await _especialidadServicio.Agregar(especialidadDto);
                _response.StatusCode = HttpStatusCode.Created;
                _response.IsExitoso = true;
                //_response.Mensaje = "Especialidad agregada correctamente";
                //_response.Resultado = especialidad;
                //return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Mensaje = ex.Message;
            }
            return Ok(_response);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(EspecialidadDto especialidadDto)
        {
            try
            {
                await _especialidadServicio.Actualizar(especialidadDto);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsExitoso = true;
                _response.Mensaje = "Especialidad actualizada correctamente";
                //return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Mensaje = ex.Message;
            }
            return Ok(_response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _especialidadServicio.Remover(id);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsExitoso = true;
                _response.Mensaje = "Especialidad eliminada correctamente";
                //return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Mensaje = ex.Message;
            }
            return Ok(_response);
        }
    }
}
