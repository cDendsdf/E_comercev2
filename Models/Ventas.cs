
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_comerce.Models
{
    public class Ventas
    {
        public int Id { get; set; }

        public string UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        public int fecha { get; set; }

        [Required]
        public int productoId { get; set; }

        [ForeignKey("productoId")]
        public Productos Productos { get; set; }

        public double Total { get; set; }

    }
}
