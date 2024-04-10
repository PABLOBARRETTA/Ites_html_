using System;
using Spectre.Console;
using Super.App.Pantallas.archivos;
using Super.App.Pantallas.buscar;
using Super.Libreria.Repositorios;

namespace Super.App.Pantallas
{
    public class PantallaPrincipal
    {
        private string[] _listadoOpciones;
        private ProductoRepositorio _productoRepositorio;

        private string _rutaArchivo;// nuevo

        public PantallaPrincipal(string rutaArchivo, ProductoRepositorio productoRepositorio)
        {
            _listadoOpciones = new string[] { "importar productos de un archivo csv", "buscar productos ", "salir" };
            _productoRepositorio = productoRepositorio;
            _rutaArchivo = rutaArchivo; // nuevo
        }

        public void MostrarPantallaPrincipal()
        {
            while (true)
            {
                AnsiConsole.Clear(); //para borrar el mensaje
            AnsiConsole.Write(  //para escribir el mensaje
                new FigletText("SUPER - ITES")
                .LeftJustified()
                .Color(Color.Red)
            );
            

                var opcionSeleccionada = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("[Red]Menu Principal[/]")
                    .AddChoices(_listadoOpciones)
                );

                switch (opcionSeleccionada.ToLower())
                {
                    case "importar productos de un archivo csv":
                        PantallaAdministracionArchivo pantallaAdministracionArchivo = new PantallaAdministracionArchivo(_rutaArchivo, _productoRepositorio);
                        pantallaAdministracionArchivo.MostrarMenu();
                        break;

                    case "buscar productos ":
                        var productos = _productoRepositorio.MostrarProductos();
                        PantallaAdministracionBusca pantallaAdministracionBusca = new PantallaAdministracionBusca(productos, _productoRepositorio);
                        pantallaAdministracionBusca.MostrarMenu(); 
                        break;

                    case "salir":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                        break;
                }
            }
        }
    }
}







