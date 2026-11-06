using InventarioRepuestos.Interfaces;
using InventarioRepuestos.Models;
using InventarioRepuestos.Repositories;
using InventarioRepuestos.Services;

IProductoRepository repositorio = new ProductoReposiyorySQL();
InventarioService servicio = new InventarioService(repositorio);

bool continuar = true;

/**
 el bucle while permite que este switch se ejecute al menos una vez
 esto hace que se muestre el menu y cuando el usuario quiera salir,
 deba presionar 0 el cual pone la variable "continuar" como false,
 esto para el bucle while y se termina el programa
*/
while (continuar)
{
    Console.WriteLine("\n===== INVENTARIO DE REPUESTOS =====");
    Console.WriteLine("1. Registrar producto");
    Console.WriteLine("2. Listar productos");
    Console.WriteLine("3. Buscar producto");
    Console.WriteLine("4. Modificar precio");
    Console.WriteLine("5. Entrada de inventario");
    Console.WriteLine("6. Salida de inventario");
    Console.WriteLine("7. Valor total del inventario");
    Console.WriteLine("8. Buscar por nombre");
    Console.WriteLine("9. Buscar productos con stock bajo");
    Console.WriteLine("0. Salir");
    Console.Write("Opción: ");

    /**
    A este switch se le envia el valor que escribe el usuario y ejecuta el case
    correspondiente, si no existe ese case definido se va al default el cual es un
    mensaje de opcion invalida
    */
    switch (Console.ReadLine())
    {
        case "1":
            string codigo = LeerTexto("Código: ");
            string nombre = LeerTexto("Nombre: ");
            decimal precio = LeerDecimal("Precio (Q): ");
            int stock = LeerEntero("Stock inicial: ");
            string categoria = LeerTexto("Categoria: ");

            var nuevo = new Producto
            {
                Codigo = codigo,
                Nombre = nombre,
                Precio = precio,
                Stock = stock,
                Categoria = categoria
            };

            Console.WriteLine(servicio.AgregarProducto(nuevo)
                ? "Producto registrado." : "Datos inválidos o código duplicado.");
            break;

        case "2":
            var productos = servicio.ObtenerTodos();

            if (productos.Count == 0)
                Console.WriteLine("No hay productos.");

            foreach (var p in productos)
                Console.WriteLine($"{p.Codigo} | {p.Nombre} | Q{p.Precio:F2} | Stock: {p.Stock} | Categoria: {p.Categoria}");
            break;

        case "3":
            MostrarProducto(servicio.BuscarPorCodigo(LeerTexto("Código a buscar: ")));
            break;

        case "4":
            string codPrecio = LeerTexto("Código: ");
            decimal nuevoPrecio = LeerDecimal("Nuevo precio: ");

            Console.WriteLine(servicio.ActualizarPrecio(codPrecio, nuevoPrecio)
                ? "Precio actualizado." : "No se pudo actualizar.");
            break;

        case "5":
            string codEntrada = LeerTexto("Código: ");
            int entrada = LeerEntero("Cantidad de entrada: ");

            Console.WriteLine(servicio.RegistrarEntrada(codEntrada, entrada)
                ? "Entrada registrada." : "Entrada rechazada.");
            break;

        case "6":
            string codSalida = LeerTexto("Código: ");
            int salida = LeerEntero("Cantidad de salida: ");

            Console.WriteLine(servicio.RegistrarSalida(codSalida, salida)
                ? "Salida registrada." : "Salida rechazada: revisa stock y cantidad.");
            break;

        case "7":
            //.Sum suma el total de CalcularValorInventario de todos los objetos creados
            decimal total = servicio.CalcularValorTotal();
            Console.WriteLine($"Valor total: Q{total:F2}");
            break;

        case "8":
            MostrarProducto(servicio.BuscarPorNombre(LeerTexto("Nombre a buscar: ")));
            break;

        case "9":
            var productosConStockBajo = servicio.BuscarPorStockBajo();

            if (productosConStockBajo.Count == 0)
                Console.WriteLine("No hay productos.");

            foreach (var p in productosConStockBajo)
                Console.WriteLine($"{p.Codigo} | {p.Nombre} | Q{p.Precio:F2} | Stock: {p.Stock} | Categoria: {p.Categoria}");
            break;

        case "0":
            continuar = false;
            Console.WriteLine("Hasta luego.");
            break;

        default:
            Console.WriteLine("Opción no válida.");
            break;
    }
}

static string LeerTexto(string mensaje)
{
    while (true)
    {
        Console.Write(mensaje);
        string valor = (Console.ReadLine() ?? "").Trim();
        if (!string.IsNullOrWhiteSpace(valor)) return valor;
        Console.WriteLine("El dato es obligatorio.");
    }
}

static int LeerEntero(string mensaje)
{
    while (true)
    {
        Console.Write(mensaje);
        //TryParse convierte valores, en este caso de texto a numero entero 
        if (int.TryParse(Console.ReadLine(), out int numero)) return numero;
        Console.WriteLine("Ingresa un número entero válido.");
    }
}

static decimal LeerDecimal(string mensaje)
{
    while (true)
    {
        Console.Write(mensaje);
        if (decimal.TryParse(Console.ReadLine(), out decimal numero)) return numero;
        Console.WriteLine("Ingresa un número decimal válido.");
    }
}

static void MostrarProducto(Producto? producto)
{
    if (producto == null)
    {
        Console.WriteLine("Producto no encontrado.");
        return;
    }

    Console.WriteLine($"{producto.Codigo} - {producto.Nombre}");
    Console.WriteLine($"Precio: Q{producto.Precio:F2}; Stock: {producto.Stock}, Categoria: {producto.Categoria}");
}
