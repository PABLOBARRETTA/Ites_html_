using Super.Libreria.Modelos;
using Super.Libreria.Repositorios;

namespace Super.Libreria.Negocio
{
    public class ProductoServicio
    {
        private readonly ProductoRepositorio _productoRepositorio;

        public ProductoServicio()
        {
            _productoRepositorio = new ProductoRepositorio();
        }

        public List<Producto> MostrarProductos()
        {
            return _productoRepositorio.ObtenerProductos();
        }
        
         public Producto ObtenerProductoPorDescripcion(string descripcion)
        {
            return _productoRepositorio.ObtenerProductoPorDescripcion(descripcion);
        } 

        public void AgregarProductoDesdeArchivo(string rutaArchivo)
        {
            var pantallaAdministracionArchivo = new PantallaAdministracionArchivo(rutaArchivo, _productoRepositorio);
            pantallaAdministracionArchivo.MostrarMenu();
           
        }


        

         public void CargarArchivo(Producto producto)
        {
            _productoRepositorio.CrearProducto(producto);
            
        } 
    }

    internal class PantallaAdministracionArchivo
    {
        private string rutaArchivo;
        private ProductoRepositorio productoRepositorio;

        public PantallaAdministracionArchivo(string rutaArchivo, ProductoRepositorio productoRepositorio)
        {
            this.rutaArchivo = rutaArchivo;
            this.productoRepositorio = productoRepositorio;
        }

        internal void MostrarMenu()
        {
            throw new NotImplementedException();
        }

    }

} 














