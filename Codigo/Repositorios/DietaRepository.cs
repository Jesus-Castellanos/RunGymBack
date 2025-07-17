using RunGym.Models;
using RunGym.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using RunGym.Run;

namespace TuProyecto.Repositories
{
    public class DietaRepository : IDietaRepository
    {
        private readonly RunGymContext _context;

        public DietaRepository(RunGymContext context)
        {
            this._context = context;
        }

        public async Task<List<Dieta>> GetDieta()
        {
            var data = await _context.Dieta.ToListAsync();
            return data;
        }

        public async Task<bool> PostDieta(Dieta dieta)
        {
            await _context.Dieta.AddAsync(dieta); // Usar 'context'
            await _context.SaveChangesAsync(); // Corregir 'SaveAsync' por 'SaveChangesAsync'
            return true;
        }

        public async Task<bool> PutDieta(Dieta dieta)
        {
            _context.Dieta.Add(dieta);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDieta(int id)
        {
            var dieta = await _context.Dieta.FindAsync(id); // Usar 'context' en lugar de '_context'
            if (dieta == null) return false; // Si no existe, devolver 'false'

            _context.Dieta.Remove(dieta); // Usar 'context'
            await _context.SaveChangesAsync(); // Corregir 'SaveAsync' por 'SaveChangesAsync'
            return true;
        }
        public async Task<List<Dieta>> GetTipoDieta(int Id)
        {
            return await _context.Dieta
                .Include(d => d.TipoDieta)
                .Where(d => d.TipoDietaId == Id)
                .ToListAsync();
        }
    }
}