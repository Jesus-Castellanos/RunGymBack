using Microsoft.EntityFrameworkCore;
using RunGym.Repositorios;
using RunGym.Repositorios.Interfaces;
using RunGym.Run;
using TuProyecto.Repositories;

namespace RunGym.API
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddExternal(this IServiceCollection services, IConfiguration _configuration)
        {
            string connectionString = _configuration["ConnectionStrings:SQLConnectionStrings"];

            services.AddDbContext<RunGymContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            services.AddScoped<IDetallesEjercicio, DetallesEjercicioRepository>();
            services.AddScoped<IMetasRepository, MetasRepository>();
            services.AddScoped<IEjerciciosRepository, EjerciciosRepository>();
            services.AddScoped<IDietaRepository, DietaRepository>();
            services.AddScoped<IPasosEjercicioRepository, PasosEjercicioRepository>();
            services.AddScoped<ITipoDietaRepository, TipoDietaRepository>();
            services.AddScoped<IErroresReportados, ErroresReportadosRepository>();
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<IEmailServiceReposytory, EmailService>();

            return services;
        }
    }
}
