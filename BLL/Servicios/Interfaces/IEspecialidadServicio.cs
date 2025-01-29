using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Serivicios.Interfaces
{
    public interface IEspecialidadServicio
    {
        Task<IEnumerable<EspecialidadDto>> ObtenerEspecialidades();
        Task<EspecialidadDto> Agregar(EspecialidadDto especialidadDto);

        Task Actualizar(EspecialidadDto especialidadDto);

        Task Remover(int id);
    }
}
