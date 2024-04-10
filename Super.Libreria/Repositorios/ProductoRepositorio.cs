 using Microsoft.EntityFrameworkCore;
using Super.Libreria.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Super.Libreria.Repositorios
{
    public class ProductoRepositorio
    {
        private readonly SuperContexto _superContexto;

        public ProductoRepositorio()
        {
            _superContexto = SuperContexto.CrearInstancia();
        }

        internal List<Producto> ObtenerProductos()
        {
            return _superContexto.Producto.ToList();
        }

        public void CrearProducto(Producto producto)
        {
            try
            {
                _superContexto.Producto.Add(producto);
                _superContexto.SaveChanges();
                Console.WriteLine("Producto creado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear producto: {ex.Message}");

                // Imprimir la excepción interna si está disponible
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
        }

        public void ActualizarProducto(Producto producto)
        {
            _superContexto.Producto.Update(producto);
            _superContexto.SaveChanges();
        }

        public Producto BuscarProductoPorCodigoEan(string codigoEan)//BuscarProductoPorId
        {
            return _superContexto.Producto.FirstOrDefault(p => p.Codigo_Ean == codigoEan);
        }

         public Producto ObtenerProductoPorDescripcion(string Codigo_Ean)
        {
            return _superContexto.Producto.FirstOrDefault(p => p.Codigo_Ean == Codigo_Ean);
        }
 
        // Nuevo método para buscar producto por su clave primaria
         public Producto ObtenerProductoPorCodigoEan(int id)//int id
        {
            return _superContexto.Producto.Find(id);
        }

        public void GuardarCambios()
        {
            try
            {
                _superContexto.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar cambios: {ex.Message}");
                // Aquí puedes agregar el manejo de errores específico según tus necesidades.
            }
        }

        public List<Producto> MostrarProductos() // permite ver todos los productos
        {
            return _superContexto.Producto.ToList();
        }
    }
} 

 




