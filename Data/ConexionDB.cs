using Microsoft.EntityFrameworkCore;
using InventarioRepuestos.Models;

namespace InventarioRepuestos.Data;
public class ConexionDB : DbContext
{
    public DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=ASUS-UX430;Initial Catalog=InventarioRepuestos;User ID=admin_javier;Password=admin_javier;TrustServerCertificate=True;");
    }
}