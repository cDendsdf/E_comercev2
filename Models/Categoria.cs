using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace E_comerce.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        
        [Required(ErrorMessage ="El campo Categoria es obligatorio")]
        [Remote(action: "ValidarNombre", controller: "Categoria")]
        public string NombreCategoria { get; set; }

    }
}
