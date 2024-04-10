using Super.App.Pantallas;
using Super.Libreria.Repositorios;



ProductoRepositorio productoRepositorio = new ProductoRepositorio();
string rutaArchivo = ""; // Puedes definir una ruta de archivo válida si es necesario
PantallaPrincipal pantallaPrincipal = new PantallaPrincipal(rutaArchivo, productoRepositorio);
pantallaPrincipal.MostrarPantallaPrincipal();



    

    

