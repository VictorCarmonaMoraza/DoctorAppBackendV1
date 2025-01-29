using Data.DBContext;
using Data.Interfaces.IRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio
{
    public class Repositorio<T> : IRepositorioGenerico<T> where T : class
    {

        public readonly ApplicationDbContext _contexto;
        private DbSet<T> _entidades;

        public Repositorio(ApplicationDbContext contexto)
        {
            _contexto = contexto;
            _entidades = contexto.Set<T>();
        }


        public async Task Agregar(T entidad)
        {
            await _entidades.AddAsync(entidad);
        }

        public void Eliminar(T entidad)
        {
            _contexto.Remove(entidad);
        }

        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null)
        {
            IQueryable<T> consulta = _entidades;
            if (filtro != null)
            {
                consulta = consulta.Where(filtro);
            }

            if (incluirPropiedades != null)
            {
                foreach (var propiedad in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    consulta = consulta.Include(propiedad);
                }
            }

            return await consulta.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string incluirPropiedades = null)
        {
            IQueryable<T> consulta = _entidades;
            if (filtro != null)
            {
                consulta = consulta.Where(filtro);
            }

            if (incluirPropiedades != null)
            {
                foreach (var propiedad in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    consulta = consulta.Include(propiedad);
                }
            }

            if (orderby != null)
            {
                return await orderby(consulta).ToListAsync();
            }
            return await consulta.ToListAsync();
        }
    }
}
