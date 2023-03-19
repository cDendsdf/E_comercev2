using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_comerce.Models.ViewModels
{
    public class PedidosViewModel
    {
       public IList<Productos> Productos { get; set; }   
       public IList<Usuario> Usuarios { get; set; }   
        
        
    }
}
