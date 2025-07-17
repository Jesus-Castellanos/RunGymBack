using RunGym.Models;
using RunGym.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using RunGym.Run;

namespace RunGym.Repositorios
{
    public class TipoDietaRepository : ITipoDietaRepository
    {
        private readonly RunGymContext _context;

        public TipoDietaRepository(RunGymContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoDieta>> GetAllAsync()
        {
            return await _context.TipoDieta.ToListAsync();
        }

        public async Task<TipoDieta?> GetByIdAsync(int id)
        {
            return await _context.TipoDieta.FindAsync(id);
        }

        public async Task<TipoDieta> CreateAsync(TipoDieta tipoDieta)
        {
            _context.TipoDieta.Add(tipoDieta);
            await _context.SaveChangesAsync();
            return tipoDieta;
        }

        public async Task UpdateAsync(TipoDieta tipoDieta)
        {
            _context.Entry(tipoDieta).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tipoDieta = await _context.TipoDieta.FindAsync(id);
            if (tipoDieta != null)
            {
                _context.TipoDieta.Remove(tipoDieta);
                await _context.SaveChangesAsync();
            }
        }
    }
}
