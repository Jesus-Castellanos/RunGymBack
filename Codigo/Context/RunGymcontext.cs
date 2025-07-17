using Microsoft.EntityFrameworkCore;
using RunGym.Models;

namespace RunGym.Run
{
    public class RunGymContext : DbContext
    {
        public RunGymContext(DbContextOptions<RunGymContext> options) : base(options)
        { }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<DetallesEjercicio> DetallesEjercicios { get; set; }
        public DbSet<Metas> Metas { get; set; }
        public DbSet<Ejercicios> Ejercicios { get; set; }
        public DbSet<Dieta> Dieta { get; set; }
        public DbSet<TipoDieta> TipoDieta { get; set; }
        public DbSet<ErroresReportados> ErroresReportados { get; set; }
        public DbSet<PasosEjercicio> PasosEjercicios { get; set; }
        public DbSet<Rol> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntityConfiguration(modelBuilder);
        }

        private void EntityConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuarios>().ToTable("Usuarios");
            modelBuilder.Entity<Usuarios>().HasKey(u => u.Id);
            modelBuilder.Entity<Usuarios>().Property(u => u.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Usuarios>().Property(u => u.Nombre).HasColumnName("Nombre");
            modelBuilder.Entity<Usuarios>().Property(u => u.Apellido).HasColumnName("Apellido");
            modelBuilder.Entity<Usuarios>().Property(u => u.TipoDocumento).HasColumnName("TipoDocumento");
            modelBuilder.Entity<Usuarios>().Property(u => u.Documento).HasColumnName("Documento");
            modelBuilder.Entity<Usuarios>().Property(u => u.Correo).HasColumnName("Correo");
            modelBuilder.Entity<Usuarios>().Property(u => u.Contraseña).HasColumnName("Contraseña");
            modelBuilder.Entity<Usuarios>().Property(u => u.Celular).HasColumnName("Celular");
            modelBuilder.Entity<Usuarios>().Property(u => u.Genero).HasColumnName("Genero");
            modelBuilder.Entity<Usuarios>().Property(u => u.FechaNacimiento).HasColumnName("FechaNacimiento");
            modelBuilder.Entity<Usuarios>().Property(u => u.Peso).HasColumnName("Peso");
            modelBuilder.Entity<Usuarios>().Property(u => u.Altura).HasColumnName("Altura");
            modelBuilder.Entity<Usuarios>().Property(u => u.HorasSueno).HasColumnName("HorasSueno");
            modelBuilder.Entity<Usuarios>().Property(u => u.ConsumoAgua).HasColumnName("ConsumoAgua");
            modelBuilder.Entity<Usuarios>().Property(u => u.FechaRegistro).HasColumnName("FechaRegistro");
            modelBuilder.Entity<Usuarios>().Property(u => u.CodigoVerificacion).HasColumnName("CodigoVerificacion");
            modelBuilder.Entity<Usuarios>().Property(u => u.CodigoExpira).HasColumnName("CodigoExpira");
            modelBuilder.Entity<Usuarios>().Property(u => u.RolId).HasColumnName("IdRol");

            modelBuilder.Entity<Usuarios>().HasOne(u => u.Rol)
                        .WithMany(r => r.Usuarios)
                        .HasForeignKey(u => u.RolId);

            modelBuilder.Entity<Rol>().ToTable("Roles");
            modelBuilder.Entity<Rol>().HasKey(r => r.Id);
            modelBuilder.Entity<Rol>().Property(r => r.NombreRol).HasColumnName("Roles");

            modelBuilder.Entity<Metas>().ToTable("Metas");
            modelBuilder.Entity<Metas>().HasKey(u => u.Id);
            modelBuilder.Entity<Metas>().Property(u => u.IdUsuario).HasColumnName("IdUsuario");
            modelBuilder.Entity<Metas>().Property(u => u.MetaPrincipal).HasColumnName("MetaPrincipal");
            modelBuilder.Entity<Metas>().Property(u => u.CuerpoActual).HasColumnName("CuerpoActual");
            modelBuilder.Entity<Metas>().Property(u => u.CuerpoDeseado).HasColumnName("CuerpoDeseado");
            modelBuilder.Entity<Metas>().Property(u => u.Descripción).HasColumnName("Descripción");
            modelBuilder.Entity<Metas>().Property(u => u.FechaObjetivo).HasColumnName("FechaObjetivo");

            modelBuilder.Entity<DetallesEjercicio>().ToTable("DetallesEjercicio");
            modelBuilder.Entity<DetallesEjercicio>().HasKey(u => u.Id);
            modelBuilder.Entity<DetallesEjercicio>().Property(u => u.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<DetallesEjercicio>().Property(u => u.EjercicioId).HasColumnName("EjercicioId");
            modelBuilder.Entity<DetallesEjercicio>().Property(u => u.Repeticiones).HasColumnName("Repeticiones");
            modelBuilder.Entity<DetallesEjercicio>().Property(u => u.Descanso).HasColumnName("Descanso");
            modelBuilder.Entity<DetallesEjercicio>().Property(u => u.Cuidados).HasColumnName("Cuidados");
            modelBuilder.Entity<DetallesEjercicio>().Property(u => u.URLVideo).HasColumnName("URLVideo");

            modelBuilder.Entity<Ejercicios>().ToTable("Ejercicios");
            modelBuilder.Entity<Ejercicios>().HasKey(u => u.Id);
            modelBuilder.Entity<Ejercicios>().Property(u => u.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Ejercicios>().Property(u => u.Nombre).HasColumnName("Nombre");
            modelBuilder.Entity<Ejercicios>().Property(u => u.Categoria).HasColumnName("Categoria");
            modelBuilder.Entity<Ejercicios>().Property(u => u.ImagenURL).HasColumnName("ImagenURL");

            modelBuilder.Entity<PasosEjercicio>().ToTable("PasosEjercicio");
            modelBuilder.Entity<PasosEjercicio>().HasKey(u => u.Id);
            modelBuilder.Entity<PasosEjercicio>().Property(u => u.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<PasosEjercicio>().Property(u => u.EjercicioId).HasColumnName("EjercicioId");
            modelBuilder.Entity<PasosEjercicio>().Property(u => u.NumeroPaso).HasColumnName("NumeroPaso");
            modelBuilder.Entity<PasosEjercicio>().Property(u => u.DescripcionPaso).HasColumnName("DescripcionPaso");



            modelBuilder.Entity<TipoDieta>().ToTable("TipoDieta");
            modelBuilder.Entity<TipoDieta>().HasKey(t => t.Id);
            modelBuilder.Entity<TipoDieta>().Property(t => t.Nombre).HasColumnName("Nombre");

            modelBuilder.Entity<Dieta>().ToTable("Dieta");
            modelBuilder.Entity<Dieta>().HasKey(d => d.Id);
            modelBuilder.Entity<Dieta>().Property(d => d.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Dieta>().Property(d => d.Desayuno).HasColumnName("Desayuno");
            modelBuilder.Entity<Dieta>().Property(d => d.Almuerzo).HasColumnName("Almuerzo");
            modelBuilder.Entity<Dieta>().Property(d => d.Cena).HasColumnName("Cena");
            modelBuilder.Entity<Dieta>().Property(d => d.Snacks).HasColumnName("Snacks");
            modelBuilder.Entity<Dieta>().Property(d => d.TipoDietaId).HasColumnName("TipoDietaId");

            modelBuilder.Entity<Dieta>()
                .HasOne(d => d.TipoDieta)
                .WithMany(td => td.Dieta)
                .HasForeignKey(d => d.TipoDietaId);



            modelBuilder.Entity<ErroresReportados>().ToTable("ErroresReportados");
            modelBuilder.Entity<ErroresReportados>().HasKey(u => u.Id);
            modelBuilder.Entity<ErroresReportados>().Property(u => u.Nombre).HasColumnName("Nombre");
            modelBuilder.Entity<ErroresReportados>().Property(u => u.Correo).HasColumnName("Correo");
            modelBuilder.Entity<ErroresReportados>().Property(u => u.Celular).HasColumnName("Celular");
            modelBuilder.Entity<ErroresReportados>().Property(u => u.Asunto).HasColumnName("Asunto");
            modelBuilder.Entity<ErroresReportados>().Property(u => u.Mensaje).HasColumnName("Mensaje");
        }
        public async Task<bool> SaveAsync()
        {
            return await SaveChangesAsync() > 0;
        }
    }
}