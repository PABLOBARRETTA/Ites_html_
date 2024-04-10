using Microsoft.EntityFrameworkCore;
using Super.Libreria.Modelos;

namespace Super.Libreria.Repositorios;

public class SuperContexto: DbContext // representa la conexion con la base de datos.
{
    private static SuperContexto instanciaContexto;
    internal object producto;

    private readonly string _cadenaConexion;
    public SuperContexto(string cadenaConexion)
    {
        _cadenaConexion = cadenaConexion;
    }

    public DbSet<Producto> Producto { get; set; } // dbset es un tipo de dato que hace referencia a la tabla

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // permite configurar en la seccion para conectarse a la base de datos
    {
        optionsBuilder.UseNpgsql(_cadenaConexion);
        base.OnConfiguring(optionsBuilder);
    }

    public static SuperContexto CrearInstancia()
    {
        if( instanciaContexto == null)
        {
            instanciaContexto = new SuperContexto(
"Server=localhost;Port=5432;Database=productosBaseDatos;User Id=postgres;Password=papachocho10");
        }
        return instanciaContexto;
    }  
}
