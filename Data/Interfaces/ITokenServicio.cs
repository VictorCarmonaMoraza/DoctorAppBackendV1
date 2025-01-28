using Models.Entities;

namespace Data.Interfaces
{
    public interface ITokenServicio
    {
        string CreatrToken(Usuario usuario);
    }
}
