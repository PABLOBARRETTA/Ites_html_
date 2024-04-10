using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;
using Spectre.Console;
using Super.Libreria.Modelos;
using Super.Libreria.Repositorios;

namespace Super.App.Pantallas.buscar
{
    public class PantallaAdministracionBusca
    {
        private string[] _listadoOpciones;
        private readonly List<Producto> _productos;
        private readonly ProductoRepositorio _productoRepositorio;

        public PantallaAdministracionBusca(List<Producto> productos, ProductoRepositorio productoRepositorio)
        {
            _listadoOpciones = new string[] { "ver productos", "buscar producto", "modificar producto", "volver" };
            _productos = productos;
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
                    .Title("[Red]Administrador de buscador [/]")
                    .AddChoices(_listadoOpciones)
                );

                switch (opcionSeleccionada.ToLower())
                {
                    case "ver productos":
                        MostrarProductos();
                        break;
                    case "buscar producto":
                        BuscarProducto();
                        break;
                    case "modificar producto":
                        ModificarProducto();
                        break;
                    case "volver":
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            }
        }

        private void MostrarProductos()
        {
            Console.WriteLine("=== Listado de Productos ===");
            foreach (var producto in _productos)
            {
                Console.WriteLine($"ID: {producto.idProducto}, Codigo Ean: {producto.Codigo_Ean}, Descripción: {producto.Descripcion}, Tipo de producto: {producto.Tipo_Producto}, Precio: {producto.Precio_Unitario}, IVA: {producto.Iva}");// /100:N2

                
            }
            Console.WriteLine();
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void BuscarProducto()
        {
            Console.Write("Ingrese la descripción del producto a buscar: ");
            string descripcion = Console.ReadLine();
            var productosEncontrados = _productos.Where(p => p.Descripcion.ToLower().Contains(descripcion.ToLower())).ToList();
            if (productosEncontrados.Any())
            {
                Console.WriteLine("\n=== Productos Encontrados ===");
                foreach (var producto in productosEncontrados)
                {
                    Console.WriteLine($"ID: {producto.idProducto}, Codigo Ean: {producto.Codigo_Ean}, Descripción: {producto.Descripcion}, Tipo de producto: {producto.Tipo_Producto}, Precio:  {producto.Precio_Unitario}, IVA: {producto.Iva}"); ///100:N2
                }

                Console.Write("\n¿Desea modificar algún producto? (S/N): ");
                string respuesta = Console.ReadLine().ToLower();
                if (respuesta == "s")
                {
                    ModificarProducto();
                }
            }
            else
            {
                Console.WriteLine("No se encontraron productos con esa descripción.");
            }
        }

        private void ModificarProducto()
        {
            Console.Write("Ingrese el ID del producto a modificar: ");
            if (int.TryParse(Console.ReadLine(), out int idProducto))
            {
                var productoModificar = _productos.FirstOrDefault(p => p.idProducto == idProducto);
                if (productoModificar != null)
                {
                    Console.WriteLine("\nSeleccione el campo que desea modificar:");
                    Console.WriteLine("1. Cogigo ean");
                    Console.WriteLine("2. Descripción");
                    Console.WriteLine("3. Tipo de producto");
                    Console.WriteLine("4. Precio unitario");
                    Console.WriteLine("5. IVA");
                    Console.Write("Opción: ");
                    string opcion = Console.ReadLine();

                    try
                    {
                        switch (opcion)
                        {
                            case "1":
                                Console.Write("Ingrese el nuevo código ean: ");
                                productoModificar.Codigo_Ean = Console.ReadLine();
                                Console.WriteLine("Código EAN fue modificado exitosamente.");
                                break;
                            case "2":
                                Console.Write("Ingrese la nueva descripción: ");
                                productoModificar.Descripcion = Console.ReadLine();
                                Console.WriteLine("La Descripción fue modificado exitosamente.");
                                break;
                            case "3":
                                Console.Write("Ingrese el nuevo tipo de producto: ");
                                productoModificar.Tipo_Producto = Console.ReadLine();
                                Console.WriteLine("El tipo de producto fue modificado exitosamente.");
                                break;
                            case "4":
                                Console.Write("Ingrese el nuevo precio: ");
                                if (double.TryParse(Console.ReadLine(), out double nuevoPrecio))
                                {
                                    productoModificar.Precio_Unitario = nuevoPrecio;
                                    Console.WriteLine("El precio fue modificado exitosamente.");
                                }
                                else
                                {
                                    Console.WriteLine("Precio inválido.");
                                }
                                break;
                            case "5":
                                Console.Write("Ingrese el nuevo IVA (en porcentaje): ");
                                if (double.TryParse(Console.ReadLine(), out double nuevoIVA) && nuevoIVA >= 0)
                                {
                                    productoModificar.Iva = nuevoIVA;
                                    Console.WriteLine("El iva fue modificado exitosamente.");
                                }
                                else
                                {
                                    Console.WriteLine("IVA inválido.");
                                }
                                break;
                            default:
                                Console.WriteLine("Opción no válida.");
                                break;
                        }
                        _productoRepositorio.GuardarCambios();
                        Console.WriteLine("Cambios guardados exitosamente.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al guardar cambios: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("No se encontró un producto con el ID especificado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }
    }
} 




