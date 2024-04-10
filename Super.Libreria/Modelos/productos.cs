using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Numerics;

namespace Super.Libreria.Modelos;

[Table("productos")]

public class Producto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("idproducto")]
    public int idProducto { get; set; }

    [Column("codigoean")]
    public string Codigo_Ean { get; set; }

    [Column("descripcion")]
    public string Descripcion { get; set; }

    [Column("tipoproducto")]
    public string Tipo_Producto { get; set; }

    [Column("preciounitario")]
    public double Precio_Unitario { get; set; }

    [Column("iva")]
    public double Iva {get; set; }
}
