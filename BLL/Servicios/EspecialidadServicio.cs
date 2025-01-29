using AutoMapper;
using BLL.Serivicios.Interfaces;
using Data.Interfaces.IRepositorio;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Servicios
{
    public class EspecialidadServicio : IEspecialidadServicio
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;

        public EspecialidadServicio(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
        }

        public async Task Actualizar(EspecialidadDto especialidadDto)
        {
            try
            {
                var especialidadDB = await _unidadTrabajo.Especialidad.ObtenerPrimero(e => e.Id == especialidadDto.Id);
                if (especialidadDB == null)
                    throw new TaskCanceledException("La especialidad no existe");

                especialidadDB.NombreEspecialidad = especialidadDto.NombreEspecialidad;
                especialidadDB.Descripcion = especialidadDto.Descripcion;
                especialidadDB.Estado = especialidadDto.Estado == 1 ? true : false;
                _unidadTrabajo.Especialidad.Actualizar(especialidadDB);
                await _unidadTrabajo.Guardar();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<EspecialidadDto> Agregar(EspecialidadDto especialidadDto)
        {
            try
            {
                Especialidad especialidad = new Especialidad
                {
                    NombreEspecialidad = especialidadDto.NombreEspecialidad,
                    Descripcion = especialidadDto.Descripcion,
                    Estado = especialidadDto.Estado == 1 ? true : false,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                };

                await _unidadTrabajo.Especialidad.Agregar(especialidad);
                await _unidadTrabajo.Guardar();

                if (especialidad.Id == 0)
                    throw new TaskCanceledException("La especialidad no se puede crear");
                return _mapper.Map<EspecialidadDto>(especialidad);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IEnumerable<EspecialidadDto>> ObtenerEspecialidades()
        {
            try
            {
                var lista = await _unidadTrabajo.Especialidad.ObtenerTodos(
                    orderby: e => e.OrderBy(e => e.NombreEspecialidad)
                    );
                return _mapper.Map<IEnumerable<EspecialidadDto>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Remover(int id)
        {
            try
            {
                var especialidadDB = await _unidadTrabajo.Especialidad.ObtenerPrimero(e => e.Id == id);
                if (especialidadDB == null)
                    throw new TaskCanceledException("La especialidad no existe");

                _unidadTrabajo.Especialidad.Eliminar(especialidadDB);
                await _unidadTrabajo.Guardar();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
