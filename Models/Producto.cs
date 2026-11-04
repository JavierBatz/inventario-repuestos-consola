namespace InventarioRepuestos.Models;

public class Producto
{
    public int Id { get; set; }
    public string Codigo { get; set; } = "";
    public string Nombre { get; set; } = "";
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public string Categoria  { get; set; } = "";

    public decimal CalcularValorInventario()
    {
        return Precio * Stock;
    }
}