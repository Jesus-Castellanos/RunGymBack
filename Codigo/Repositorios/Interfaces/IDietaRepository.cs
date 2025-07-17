using RunGym.Models;
namespace RunGym.Repositorios.Interfaces
{
    public interface IDietaRepository
    {
        Task<List<Dieta>> GetDieta();
        Task<List<Dieta>> GetTipoDieta(int Id);
        Task<bool> PostDieta(Dieta dieta);
        Task<bool> PutDieta(Dieta dieta);
        Task <bool>DeleteDieta(int id);
    }
}
