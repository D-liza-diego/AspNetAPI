using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AspNetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly webContext _context;
        
        public ProductController (webContext webContext)
        {
            _context = webContext;
        }
        [HttpGet]
        [Route("List")]
        public IActionResult listar()
        {
            List<Product> listProduct = new List<Product>();
            try
            {
                listProduct = _context.Products.Include(p=>p.IdcategoriaNavigation).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = listProduct });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = listProduct });
            }
        }
        [HttpGet]
        [Route("Read/{id:int}")]
        public IActionResult read(int id)
        {
            Product product = _context.Products.Find(id);
            if (product == null)
            {
                return BadRequest("No se encontró");
            }
            try
            {
                product = _context.Products.Include(p => p.IdcategoriaNavigation).Where(a => a.Idproduct == id).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = product });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = e.Message, Response = product });
            }

        }
        [HttpPost]
        [Route("Create")]
        public IActionResult create([FromBody] Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = e.Message });
            }
        }
        [HttpPut]
        [Route("Update")]
      
        public IActionResult update([FromBody] Product objeto)
        {
            Product product = _context.Products.Find(objeto.Idproduct);
            if(product==null)
            {
                return BadRequest("Producto no actualizado");
            }
            try
            {
                product.Nameproduct = objeto.Nameproduct is null ? product.Nameproduct : objeto.Nameproduct;
                product.Precio = objeto.Precio is null ? product.Precio : objeto.Precio;
                product.Cantidad=objeto.Cantidad is null? product.Cantidad: objeto.Cantidad;
                product.Idcategoria = objeto.Idcategoria is null? product.Idcategoria : objeto.Idcategoria;

                _context.Products.Update(product);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = e.Message, Response = product });
            }
        }
        [HttpDelete]
        [Route("Delete/{id:int}")]
        public IActionResult delete(int id)
        {
            Product product = _context.Products.Find(id);
            if (product == null)
            {
                return BadRequest("No se encontró");
            }
            try
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Eliminado"});
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = e.Message});
            }
           
        }
    }
}
