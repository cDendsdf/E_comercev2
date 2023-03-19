using E_comerce.Data;
using E_comerce.Models;
using E_comerce.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace E_comerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext context;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
			this.context = context;
		}


        //Mostramos los articulos en existencia
        public IActionResult Index()
        {
            HomeVM model = new HomeVM()
            {
                productos = context.Producto.Include(c => c.Categoria)
              
            };




            return View(model);
        }




        [Authorize(Roles =RUTAIMAGEN.admin)]
        public IActionResult MostrarPedidos()
        {
            PedidosViewModel detalles = new PedidosViewModel();


            detalles.Usuarios = context.Usuario.ToList();
            detalles.Productos = context.Producto.ToList(); 

       

            



            return View(detalles);
        }


        //Muestra el producto que el usuario desea comprar
        [Authorize(Roles = RUTAIMAGEN.admin)]
        public IActionResult Comprar(int id)
        {
           


            //ventas.Productos = context.Producto.FirstOrDefault(p => p.Id == id);
            var producto = context.Producto.Find(id);

            return View(producto);

        }
        //agregar ala base datos quien compro el producto y cual producto se compro con su precio 

        [HttpPost]
        [Authorize(Roles = RUTAIMAGEN.admin)]
        public IActionResult Comprar(Productos productos)
        {

            //identifica el usuario conectado
            var clain = (ClaimsIdentity)User.Identity;
            var claims = clain.FindFirst(ClaimTypes.NameIdentifier);


            //guarda los datos

            Detalles ventas = new Detalles()
            {
                UsuarioId = claims.Value,
                ProductoId = productos.Id
            };

            context.Detalle.Add(ventas);
            context.SaveChanges();
           
            return RedirectToAction("Index");

        }


        //Muestra  de la base datos quien compro el producto y cual producto se compro con su precio 
        [Authorize(Roles = RUTAIMAGEN.admin)]
        public IActionResult VerCompras(string id)
        {
      

            

           var detalles = context.Detalle.Include(x => x.usuario).Include(p => p.Producto).Where(x => x.usuario.Id == id);

            return View(detalles);

        }








        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}