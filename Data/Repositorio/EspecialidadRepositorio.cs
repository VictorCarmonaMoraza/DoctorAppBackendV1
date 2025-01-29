using Data.DBContext;
using Data.Interfaces.IRepositorio;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio
{
    public class EspecialidadRepositorio : Repositorio<Especialidad>, IEspecialidadRepositorio
    {
        private readonly ApplicationDbContext _contexto;    
        public EspecialidadRepositorio(ApplicationDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public void Actualizar(Especialidad especialidad)
        {
            var especialidadDb = _contexto.Especialidades.FirstOrDefault(x => x.Id == especialidad.Id);
            if(especialidadDb != null)
            {
                especialidadDb.NombreEspecialidad = especialidad.NombreEspecialidad;
                especialidadDb.Descripcion = especialidad.Descripcion;
                especialidadDb.Estado = especialidad.Estado;
                _contexto.SaveChanges();
            }
        }

    }
}
