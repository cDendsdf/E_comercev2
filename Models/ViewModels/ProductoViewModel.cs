using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_comerce.Models.ViewModels
{
    public class ProductoViewModel
    {
       public Productos producto { get; set; }   
        public IEnumerable<SelectListItem> DropDowmCategoria { get; set; }
        
    }
}
