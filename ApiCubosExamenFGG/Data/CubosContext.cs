using ApiCubosExamenFGG.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCubosExamenFGG.Data
{
    public class CubosContext : DbContext
    {
        public CubosContext(DbContextOptions<CubosContext> options): base(options) { }

        public DbSet<Cubo> Cubos { get; set; }
        public DbSet<UsuarioCubo> Usuarios { get; set; }
        public DbSet<CompraCubo> Compras { get; set; }
    }
}
