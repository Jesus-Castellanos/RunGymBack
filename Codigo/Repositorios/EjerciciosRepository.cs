using RunGym.Models;
using RunGym.Repositorios.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using RunGym.Run;
using RunGym.Models;

namespace RunGym.Repositorios
{
    public class EjerciciosRepository : IEjerciciosRepository
    {
        public readonly RunGymContext _context; // Usar 'context' en lugar de '_context'

        public EjerciciosRepository(RunGymContext context)
        {
            this._context = context; // Se inyecta el contexto en el constructor
        }

        public async Task<List<Ejercicios>> GetEjercicios()
        {
            var data = await _context.Ejercicios.ToListAsync(); // Usar 'context'
            return data;
        }

        public async Task<bool> PostEjercicios(Ejercicios ejercicios)
        {
            await _context.Ejercicios.AddAsync(ejercicios); // Usar 'context'
            await _context.SaveChangesAsync(); // Corregir 'SaveAsync' por 'SaveChangesAsync'
            return true;
        }

        public async Task<bool> PutEjercicios(Ejercicios ejercicios)
        {
            _context.Ejercicios.Update(ejercicios);
            await _context.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteEjercicios(int id)
        {
            var ejercicios = await _context.Ejercicios.FindAsync(id); // Usar 'context' en lugar de '_context'
            if (ejercicios == null) return false; // Si no existe, devolver 'false'

            _context.Ejercicios.Remove(ejercicios); // Usar 'context'
            await _context.SaveChangesAsync(); // Corregir 'SaveAsync' por 'SaveChangesAsync'
            return true;
        }

        public async Task<EjercicioDto?> GetDetalles(int id)
        {
            var ejercicio = await _context.Ejercicios
                .Where(e => e.Id == id)
                .Select(e => new EjercicioDto
                {
                    Nombre = e.Nombre,
                    Detalles = e.Detalles.Select(d => new DetalleDto
                    {
                        Descripcion = d.Descripcion,
                        Repeticiones = d.Repeticiones,
                        Descanso = d.Descanso,
                        Cuidados = d.Cuidados,
                        URLVideo = d.URLVideo
                    }).ToList(),
                    Pasos = e.Pasos
                        .OrderBy(p => p.NumeroPaso)
                        .Select(p => new PasoDto
                        {
                            NumeroPaso = p.NumeroPaso,
                            DescripcionPaso = p.DescripcionPaso
                        }).ToList()
                })
                .FirstOrDefaultAsync(); // ✅ método asíncrono

            return ejercicio;
        }
    }
}
