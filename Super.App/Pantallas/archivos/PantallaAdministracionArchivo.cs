using System;
using System.IO;
using Spectre.Console;
using Super.Libreria.Modelos;
using Super.Libreria.Repositorios;

namespace Super.App.Pantallas.archivos
{
    public class PantallaAdministracionArchivo
    {
        private string[] _listadoOpciones;
        private readonly ProductoRepositorio _productoRepositorio;

        public PantallaAdministracionArchivo(string rutaArchivo, ProductoRepositorio productoRepositorio)
        {
            _listadoOpciones = new string[] { "agregar", "volver" };
            _productoRepositorio = productoRepositorio;
        }

        public void MostrarMenu()
        {
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new FigletText("SUPER - ITES")
                    .LeftJustified()
                    .Color(Color.Red)
                );

                var opcionSeleccionada = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("[Red]Administrador Archivo [/]")
                    .AddChoices(_listadoOpciones)
                );

                if (opcionSeleccionada.ToLower() == "volver")
                {
                    break;
                }

                switch (opcionSeleccionada.ToLower())
                {
                    case "agregar":
                        CargarArchivo();
                        break;
                    default:
                        break;
                }
            }
        }

        void CargarArchivo()
        {
            Console.WriteLine("Ingrese la ruta del archivo CSV: ");
            string rutaArchivo = Console.ReadLine();

            if (File.Exists(rutaArchivo))
            {
                try
                {
                    string[] lineas = File.ReadAllLines(rutaArchivo);

                    // Omitir la primera línea (encabezado)
                    for (int i = 1; i < lineas.Length; i++)
                    {
                        string[] campos = lineas[i].Split(';');
                        if (campos.Length >= 6)
                        {
                            if (int.TryParse(campos[0], out int idProducto))
                            {
                                string codigoEan = campos[1];
                                string descripcion = campos[2];
                                string tipoProducto = campos[3];

                                if (double.TryParse(campos[4], out double precioUnitario) && double.TryParse(campos[5], out double iva))
                                {
                                    Console.WriteLine($"ID: {idProducto}, Código EAN: {codigoEan}, Descripción: {descripcion}, Tipo de producto: {tipoProducto}, Precio unitario: {precioUnitario}, IVA: {iva}"); //:c  :P

                                    // Verificar si el producto ya existe en la base de datos
                                    Producto productoExistente = _productoRepositorio.BuscarProductoPorCodigoEan(codigoEan);

                                    if (productoExistente == null)
                                    {
                                        // No existe un producto con el mismo Código EAN, así que lo creamos como un nuevo producto
                                        Producto nuevoProducto = new Producto
                                        {
                                            Codigo_Ean = codigoEan,
                                            Descripcion = descripcion,
                                            Tipo_Producto = tipoProducto,
                                            Precio_Unitario = precioUnitario,
                                            Iva = iva
                                        };
                                        _productoRepositorio.CrearProducto(nuevoProducto);
                                        Console.WriteLine("Nuevo producto agregado correctamente.");
                                    }
                                    else if (productoExistente.Descripcion != descripcion || productoExistente.Tipo_Producto != tipoProducto || productoExistente.Precio_Unitario != precioUnitario || productoExistente.Iva != iva)
                                    {
                                        // Si los detalles del producto existente son diferentes a los nuevos, actualizamos el producto existente
                                        productoExistente.Descripcion = descripcion;
                                        productoExistente.Tipo_Producto = tipoProducto;
                                        productoExistente.Precio_Unitario = precioUnitario;
                                        productoExistente.Iva = iva;

                                        _productoRepositorio.ActualizarProducto(productoExistente);
                                        Console.WriteLine($"Producto actualizado correctamente: Código EAN {codigoEan}");
                                    }
                                    else
                                    {
                                        // El producto existente no ha sufrido ninguna modificación
                                        Console.WriteLine($"El producto con Código EAN {codigoEan} ya está  en la base de datos.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error al convertir precio unitario o IVA.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Error al convertir ID de producto en la línea {i + 1}: '{campos[0]}'.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("La línea no tiene la cantidad esperada de campos.");
                        }
                    }
                    Console.WriteLine("El archivo CSV fue procesado correctamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al procesar el archivo CSV: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("El archivo CSV no existe.");
            }

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}












