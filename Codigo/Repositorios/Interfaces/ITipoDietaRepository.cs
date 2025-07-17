using RunGym.Models;

namespace RunGym.Repositorios.Interfaces
{
    public interface ITipoDietaRepository
    {
        Task<IEnumerable<TipoDieta>> GetAllAsync();
        Task<TipoDieta?> GetByIdAsync(int id);
        Task<TipoDieta> CreateAsync(TipoDieta tipoDieta);
        Task UpdateAsync(TipoDieta tipoDieta);
        Task DeleteAsync(int id);
    }
}
