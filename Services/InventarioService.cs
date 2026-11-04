
using InventarioRepuestos.Interfaces;
using InventarioRepuestos.Models;

namespace InventarioRepuestos.Services;

public class InventarioService
{
    private readonly IProductoRepository _repositorio;

    public InventarioService(IProductoRepository repositorio)
    {
        _repositorio = repositorio;
    }

    public bool AgregarProducto(Producto producto)
    {
        if (string.IsNullOrWhiteSpace(producto.Codigo) ||
            string.IsNullOrWhiteSpace(producto.Nombre) ||
            string.IsNullOrWhiteSpace(producto.Categoria) ||
            producto.Precio < 0 ||
            producto.Stock < 0 ||
            _repositorio.BuscarPorCodigo(producto.Codigo) != null)
            return false;

        return _repositorio.Agregar(producto);
    }

    public List<Producto> ObtenerTodos()
    {
        return _repositorio.ObtenerTodos();
    }

    public Producto? BuscarPorCodigo(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            return null;

        return _repositorio.BuscarPorCodigo(codigo);
    }

    public Producto? BuscarPorNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            return null;

        return _repositorio.BuscarPorNombre(nombre);
    }

    public bool ActualizarPrecio(string codigo, decimal precio)
    {
        if (string.IsNullOrWhiteSpace(codigo) || precio < 0)
            return false;

        return _repositorio.ActualizarPrecio(codigo, precio);
    }

    public bool RegistrarEntrada(string codigo, int cantidad)
    {
        if (string.IsNullOrWhiteSpace(codigo) || cantidad <= 0)
            return false;

        return _repositorio.RegistrarEntrada(codigo, cantidad);
    }

    public bool RegistrarSalida(string codigo, int cantidad)
    {
        if (string.IsNullOrWhiteSpace(codigo) || cantidad <= 0)
            return false;

        Producto? producto = _repositorio.BuscarPorCodigo(codigo);

        if (producto == null || cantidad > producto.Stock)
            return false;

        return _repositorio.RegistrarSalida(codigo, cantidad);
    }

    public List<Producto> BuscarPorStockBajo()
    {
        return _repositorio.BuscarPorStockBajo();
    }

    public decimal CalcularValorTotal()
    {
        return _repositorio.ObtenerTodos()
            .Sum(p => p.CalcularValorInventario());
    }
}
