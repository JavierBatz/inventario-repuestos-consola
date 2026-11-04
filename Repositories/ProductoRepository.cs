using InventarioRepuestos.Interfaces;
using InventarioRepuestos.Models;

namespace InventarioRepuestos.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly List<Producto> _productos = new();

    public bool Agregar(Producto producto)
    {
        if (string.IsNullOrWhiteSpace(producto.Codigo) ||
            string.IsNullOrWhiteSpace(producto.Nombre) ||
            string.IsNullOrWhiteSpace(producto.Categoria) ||
            producto.Precio < 0 || producto.Stock < 0 ||
            BuscarPorCodigo(producto.Codigo) != null)
            return false;

        _productos.Add(producto);
        return true;
    }

    public Producto? BuscarPorCodigo(string codigo)
    {
        return _productos.FirstOrDefault(p =>
            p.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase));
    }

    public Producto? BuscarPorNombre(string nombre)
    {
        return _productos.FirstOrDefault(p =>
            p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
    }

    public List<Producto> ObtenerTodos()
    {
        return new List<Producto>(_productos);
    }

    public List<Producto> BuscarPorStockBajo()
    {
        List<Producto> filtrados = _productos.Where(p => p.Stock <= 5).ToList();
        return filtrados;
    }

    public bool ActualizarPrecio(string codigo, decimal precio)
    {
        Producto? producto = BuscarPorCodigo(codigo);
        if (producto == null || precio < 0) return false;

        producto.Precio = precio;
        return true;
    }

    public bool RegistrarEntrada(string codigo, int cantidad)
    {
        Producto? producto = BuscarPorCodigo(codigo);
        if (producto == null || cantidad <= 0) return false;

        producto.Stock += cantidad;
        return true;
    }

    public bool RegistrarSalida(string codigo, int cantidad)
    {
        Producto? producto = BuscarPorCodigo(codigo);
        if (producto == null || cantidad <= 0 ||
            cantidad > producto.Stock) return false;

        producto.Stock -= cantidad;
        return true;
    }
}
