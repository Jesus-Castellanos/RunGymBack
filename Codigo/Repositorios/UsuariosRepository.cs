using RunGym.Models;
using RunGym.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using RunGym.Run;

namespace RunGym.Repositorios
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly RunGymContext _context;

        public UsuariosRepository(RunGymContext context)
        {
            this._context = context;
        }

        public async Task<List<Usuarios>> GetUsuarios()
        {
            var data = await _context.Usuarios.ToListAsync();
            return data;
        }
        public async Task<bool> PostUsuarios(Usuarios usuarios)
        {
            await _context.Usuarios.AddAsync(usuarios);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> PutUsuarios(Usuarios usuarios)
        {
            _context.Usuarios.Update(usuarios);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteUsuarios(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return false;
            }

            // Obtener y eliminar las metas asociadas al usuario
            var metas = await _context.Metas.Where(m => m.IdUsuario == id).ToListAsync();
            if (metas.Any())
            {
                _context.Metas.RemoveRange(metas);
            }

            // Eliminar el usuario
            _context.Usuarios.Remove(usuario);

            await _context.SaveChangesAsync();
            return true;
        }


        // Obtener un usuario por correo
        public async Task<Usuarios> GetUsuarioByCorreoAsync(string correo)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
        }

        // Obtener un usuario por el Codigo de Verificacion recuperación
        public async Task<Usuarios> GetUsuarioByCodigoAsync(string codigo)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.CodigoVerificacion == codigo);
        }

        // Actualizar un usuario
        public async Task<bool> UpdateUsuarioAsync(Usuarios usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}