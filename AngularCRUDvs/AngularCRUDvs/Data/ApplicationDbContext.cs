using System;
using AngularCRUDvs.Entidades;
using Microsoft.EntityFrameworkCore;

namespace AngularCRUDvs.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");

            var configuration = configurationBuilder.Build();
            var connectionString = configuration.GetConnectionString("dbDocker");
            options.UseSqlServer(connectionString);

            //string dbPath = System.IO.Path.Combine(Environment.CurrentDirectory, "app.db");
            //string connectionString = $"Data Source={dbPath}";
            //options.UseSqlite(connectionString);
        }


        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Recibo> Recibo { get; set; }
        public DbSet<Concepto> Concepto { get; set; }
        public DbSet<ReciboConcepto> ReciboConcepto { get; set; }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Unidad> Unidad { get; set; }
        public DbSet<ReciboPago> ReciboPago { get; set; }

    }
}

