using Data.DBContext;
using Data.Interfaces.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _contexto;
        public IEspecialidadRepositorio Especialidad { get; private set; }

        public UnidadTrabajo(ApplicationDbContext contexto)
        {
            _contexto = contexto;
            Especialidad = new EspecialidadRepositorio(_contexto);
        }

        public async Task Guardar()
        {
            _contexto.SaveChanges();
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}
