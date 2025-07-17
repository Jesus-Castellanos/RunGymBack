using RunGym.Models;
using RunGym.Repositorios.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RunGym.Run;

namespace RunGym.Repositorios
{
    public class RolRepository : IRolRepository
    {
        private readonly RunGymContext _context;

        public RolRepository(RunGymContext context)
        {
            this._context = context;
        }

        public async Task<List<Rol>> GetRol()
        {
            var data = await _context.Roles.ToListAsync();
            return data;
        }

        public async Task<Rol> GetRolById(int id)
        {
            var data = await _context.Roles.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<Rol> GetRolByName(string nombre)
        {
            var data = await _context.Roles.Where(x => x.NombreRol == nombre).FirstOrDefaultAsync();
            return data;
        }

        public async Task<bool> PostRol(Rol rol)
        {
            await _context.Roles.AddAsync(rol);
            await _context.SaveAsync();
            return true;
        }

        public async Task<bool> PutRol(Rol rol)
        {
            _context.Update(rol);
            await _context.SaveAsync();
            return true;

        }

        public async Task<bool> DeleteRol(Rol rol)
        {
            _context.Roles.Remove(rol);
            await _context.SaveAsync();
            return true;
        }
    }
}
