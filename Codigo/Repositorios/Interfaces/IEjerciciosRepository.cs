using RunGym.Models;
using RunGym.Models;
namespace RunGym.Repositorios.Interfaces

{
    public interface IEjerciciosRepository
    {
        Task<List<Ejercicios>> GetEjercicios();
        Task<EjercicioDto> GetDetalles(int id);
        Task<bool> PostEjercicios(Ejercicios ejercicios);
        Task<bool> PutEjercicios(Ejercicios ejercicios);
        Task<bool> DeleteEjercicios(int id);
    }
}
