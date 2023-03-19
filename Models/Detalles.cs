using System.ComponentModel.DataAnnotations.Schema;

namespace E_comerce.Models
{
    public class Detalles
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public int ProductoId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario usuario { get; set; }
        [ForeignKey("ProductoId")]
        public Productos Producto { get; set; }     
    }
}
