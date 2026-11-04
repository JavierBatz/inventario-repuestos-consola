using InventarioRepuestos.Models;

namespace InventarioRepuestos.Interfaces;

public interface IProductoRepository
{
    bool Agregar(Producto producto);
    Producto? BuscarPorCodigo(string codigo);
    Producto? BuscarPorNombre(string nombre);
    List<Producto> ObtenerTodos();
    bool ActualizarPrecio(string codigo, decimal precio);
    bool RegistrarEntrada(string codigo, int cantidad);
    bool RegistrarSalida(string codigo, int cantidad);
    List<Producto> BuscarPorStockBajo();
}