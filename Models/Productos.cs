﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_comerce.Models
{
    public class Productos
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre del Producto es requerido")]
        public string NombreProducto { get; set; }

        [Required(ErrorMessage = "Descripcion Corta es Requerida")]
        public string DescripcionCorta { get; set; }

        [Required(ErrorMessage = "Descripcion del Producto es requerida")]
        public string DescripcionProducto { get; set; }

        [Required(ErrorMessage = "El Precio del Producto es Requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "El Precio debe de ser Mayor a cero")]
        public Decimal Precio { get; set; }

        public int Existencia { get; set; }
        public string ImagenUrl { get; set; }

        // Foreign Key


        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual Categoria? Categoria { get; set; }

    }
}
