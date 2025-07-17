using RunGym.Models;
using RunGym.Repositorios.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RunGym.Run;

namespace RunGym.Repositorios
{
    public class MetasRepository : IMetasRepository
    {
        private readonly RunGymContext _context;

        public MetasRepository(RunGymContext context)
        {
            this._context = context;
        }

        public async Task<List<Metas>> GetMetas()
        {
            var data = await _context.Metas.ToListAsync();
            return data;
        }

        public async Task<Metas> GetMetasById(int id)
        {
            var data = await _context.Metas.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<Metas> GetMetasByName(string nombre)
        {
            var data = await _context.Metas.Where(x => x.MetaPrincipal == nombre).FirstOrDefaultAsync();
            return data;
        }

        public async Task<bool> PostMetas(Metas metas)
        {
            var usuario = await _context.Usuarios.FindAsync(metas.IdUsuario);
            if (usuario == null)
            {
                throw new Exception("El usuario especificado no existe.");
            }

            await _context.Metas.AddAsync(metas);
            await _context.SaveAsync();
            return true;
        }

        public async Task<bool> PutMetas(Metas metas)
        {
            _context.Update(metas);
            await _context.SaveAsync();
            return true;

        }

        public async Task<bool> DeleteMetas(Metas metas)
        {
            _context.Metas.Remove(metas);
            await _context.SaveAsync();
            return true;
        }
    }
}
