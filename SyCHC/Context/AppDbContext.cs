using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SyCHC.Models;

namespace SyCHC.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Acceso> Acceso { get; set; }
        public DbSet<Actividad> Actividad { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Clientes_Usuario> Clientes_Usuario { get; set; }
        public DbSet<Consultor> Consultor { get; set; }
        public DbSet<Consultores_Proyecto> Consultores_Proyecto { get; set; }
        public DbSet<Etapa> Etapa { get; set; }
        public DbSet<Etapas_Proyecto> Etapas_Proyecto { get; set; }
        public DbSet<Funcion> Funcion { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Perfiles_Consultor> Perfiles_Consultor { get; set; }
        public DbSet<Proyecto> Proyecto { get; set; }
        public DbSet<Sesion> Sesion { get; set; }
        public DbSet<TiposUsuario> TiposUsuario { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        //Vistas
        public DbSet<Lista_Perfiles_Por_Consultor> Lista_Perfiles_Por_Consultor { get; set; }
        public DbSet<Lista_Etapas_De_Proyecto> Lista_Etapas_De_Proyecto { get; set; }
        public DbSet<Lista_Proyectos_Cliente_Consultor> Lista_Proyectos_Cliente_Consultor { get; set; }
    }
}
