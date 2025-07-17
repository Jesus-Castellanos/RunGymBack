using RunGym.Models;

namespace RunGym.Repositorios.Interfaces
{
    public interface IRolRepository
    {
        Task<List<Rol>> GetRol();

        Task<Rol> GetRolById(int id);

        Task<Rol> GetRolByName(string nombre);

        Task<bool> PostRol(Rol rol);

        Task<bool> PutRol(Rol rol);

        Task<bool> DeleteRol(Rol rol);

    }
}
