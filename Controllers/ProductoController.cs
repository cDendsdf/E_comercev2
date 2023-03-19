using E_comerce.Data;
using E_comerce.Models;
using E_comerce.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace E_comerce.Controllers
{


    [Authorize(Roles =RUTAIMAGEN.admin)]
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment hostEnvironment;

        public ProductoController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            this.db = db;
            this.hostEnvironment = hostEnvironment;
        }

        //Esta Accion muestra la vista index con una tabla donde se muestran todos los Producto
        public IActionResult Index()
        {

            IEnumerable<Productos> productos = db.Producto.Include(c => c.Categoria);
            return View(productos);
        }





        //UPSERT Es una Accion que sirve para actualizar campos o agregar campos validando si se pasa el ID

        public async Task<IActionResult> Upsert(int? id)
        {
            var model = new ProductoViewModel()
            {
                producto = new Productos(),
                DropDowmCategoria = DropdownCategoria(),
            };


            if (id == null)
            {
                return View(model);
            }
            else
            {
                model.producto = await db.Producto.FindAsync(id);
                if (model.producto is null)
                {
                    return NotFound();
                }
                return View(model);
            }

        }


        //Esta accion de tipo Post Realiza la correspondiente Actualizacion o Agregacion de datos en la base de datos


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductoViewModel model)
        {


            //Validamos que el model sea valido
            if (!ModelState.IsValid)
            {


                model.DropDowmCategoria = DropdownCategoria();

                return View(model);

            }
            else
            {//Comenzamos a ver si se ha cargado algun archivo en la vista

                var file = HttpContext.Request.Form.Files;

                //obtenemos la ruto principal donde se guardara la imagen en este caso la wwwRoot
                var RootPath = hostEnvironment.WebRootPath;

                //si el id de curso es igual a cero significa que crearemos un nuevo curso 
                if (model.producto.Id == 0)
                {



                    //validamos que se guarde una imagen identificando que el usuario desea agregar una imagen

                    if (file.Count > 0)
                    {
                        string Upload = RootPath + RUTAIMAGEN.RutaImagen;
                        string filename = Guid.NewGuid().ToString();
                        var extencion = Path.GetExtension(file[0].FileName);


                        using (FileStream fileStream = new FileStream(Path.Combine(Upload, filename + extencion), FileMode.Create))
                        {
                            file[0].CopyTo(fileStream);
                        }

                        model.producto.ImagenUrl = filename + extencion;
                        db.Producto.Add(model.producto);

                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        //en nuestro sistema requerimos que se seleccione una imagen por lo cual si no se hace regresamos ala misma vista pasandole el model

                        model.DropDowmCategoria = DropdownCategoria();
                        return View(model);

                    }






                }
                else
                {
                    //actualizamo un curso ya existente


                    var cursoDb = await db.Producto.AsNoTracking().FirstOrDefaultAsync(c => c.Id == model.producto.Id);


                    //Actualizamos el curso si agrega una nueva imagen se borra la anterior y se guarda la nueva
                    if (file.Count > 0)
                    {
                        string Upload = RootPath + RUTAIMAGEN.RutaImagen;
                        string filename = Guid.NewGuid().ToString();
                        string extencion = Path.GetExtension(file[0].FileName);

                        var urlimagenAnterior = Path.Combine(Upload, cursoDb.ImagenUrl);



                        if (System.IO.File.Exists(urlimagenAnterior))
                        {
                            System.IO.File.Delete(urlimagenAnterior);
                        }


                        using (FileStream fileStream = new FileStream(Path.Combine(Upload, filename + extencion), FileMode.Create))
                        {
                            file[0].CopyTo(fileStream);
                        }

                        model.producto.ImagenUrl = filename + extencion;
                        db.Producto.Update(model.producto);
                        await db.SaveChangesAsync();

                    }
                    else
                    {
                        //en nuestro sistema requerimos que se seleccione una imagen por lo cual si no se hace regresamos ala misma vista pasandole el model

                        
         
                        
                        model.producto.ImagenUrl = cursoDb.ImagenUrl;
                        db.Producto.Update(model.producto);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }

                }
                return View(model);
            


            }


            

        }

        public IActionResult Eliminar(int id)
        {
            var prod = db.Producto.Find(id);

            if (prod == null)
            {
                return NotFound();
            }
            else
            {


                var RootPath = hostEnvironment.WebRootPath;
                string Upload = RootPath + RUTAIMAGEN.RutaImagen;



                var urlimagenAnterior = Path.Combine(Upload, prod.ImagenUrl);




                if (System.IO.File.Exists(urlimagenAnterior))
                {
                    System.IO.File.Delete(urlimagenAnterior);
                }

                db.Producto.Remove(prod);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }



        public IEnumerable<SelectListItem> DropdownCategoria()
        {
            return db.Categorias.Select(c => new SelectListItem(c.NombreCategoria, c.CategoriaId.ToString()));
        }
    }
}
