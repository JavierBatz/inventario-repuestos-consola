using InventarioRepuestos.Models;
using InventarioRepuestos.Data;
using InventarioRepuestos.Interfaces;

namespace InventarioRepuestos.Repositories;


public class ProductoReposiyorySQL : IProductoRepository
{
    private readonly ConexionDB conexion = new ConexionDB();


    public bool Agregar(Producto producto)
    {
        conexion.Productos.Add(producto);
        conexion.SaveChanges();
        return true;

    }

    public Producto? BuscarPorCodigo(string codigo)
    {
        return conexion.Productos.FirstOrDefault(p => p.Codigo == codigo);
    }

    public Producto? BuscarPorNombre(string nombre)
    {
        return conexion.Productos.FirstOrDefault(p => p.Nombre == nombre);
    }

    public List<Producto> ObtenerTodos()
    {
        return conexion.Productos.ToList();
    }

    public List<Producto> BuscarPorStockBajo()
    {
        return conexion.Productos.Where(p => p.Stock <= 5).ToList();
    }

    public bool ActualizarPrecio(string codigo, decimal precio)
    {
        var producto = BuscarPorCodigo(codigo);
        
        if (producto == null)
        return false;

        producto.Precio = precio;

        conexion.Productos.Update(producto);
        conexion.SaveChanges();

        return true;
    }

    public bool RegistrarEntrada(string codigo, int cantidad)
    {
        var producto = BuscarPorCodigo(codigo);
        
        if (producto == null)
        return false;

        producto.Stock = producto.Stock + cantidad;

        conexion.Productos.Update(producto);
        conexion.SaveChanges();

        return true;
    }

    public bool RegistrarSalida(string codigo, int cantidad)
    {
        var producto = BuscarPorCodigo(codigo);
        
        if (producto == null)
        return false;

        producto.Stock = producto.Stock - cantidad;

        conexion.Productos.Update(producto);
        conexion.SaveChanges();

        return true;
    }  
}