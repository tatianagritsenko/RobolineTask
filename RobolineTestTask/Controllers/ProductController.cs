using Microsoft.AspNetCore.Mvc;
using RobolineTestTask.Database;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace RobolineTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly RobolineContext db;

        public ProductController(RobolineContext databaseContext)
        {
            db = databaseContext;
        }

        // получение всех продуктов
        [HttpGet("products")]
        public IActionResult Read()
        {
            try
            {
                var products = db.Products.ToList();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to read records from the database. " + ex.Message + " " + ex.InnerException?.Message);
            }
        }

        // получение продукта по id
        [HttpGet("products/{id}")]
        public IActionResult Read(int id)
        {
            try
            {
                var product = db.Products.FirstOrDefault(p => p.Id == id);

                if (product == null) 
                    return NotFound("The database entry doesn't exist");
         
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to read the record from the database. " + ex.Message + " " + ex.InnerException?.Message);
            }
        }

        // добавление записи о новом продукте
        [HttpPost("products")]
        public IActionResult Create(Product product)
        {
            try
            {
                if (product != null)
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                    return Ok(product);
                }

                return BadRequest("Product is NULL");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to add an entry to the database. " + ex.Message + " " + ex.InnerException?.Message);
            }                           
        }

        // обновление записи о продукте
        [HttpPut("products/{id}")]
        public IActionResult Update(int id, Product product)
        {
            try
            {
                if (product != null)
                {
                    if (product.Id == id)
                    {
                        db.Products.Update(product);
                        db.SaveChanges();

                        return Ok(product);
                    }
                    return BadRequest("The product ID in the request body doesn't match the ID in the request URL");
                }
                return BadRequest("Product is NULL");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to update an entry to the database. " + ex.Message + " " + ex.InnerException?.Message);
            }            
        }

        [HttpDelete("products/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var product = db.Products.FirstOrDefault(p => p.Id == id);

                if (product == null) 
                    return NotFound("The database entry doesn't exist");

                db.Products.Remove(product);
                db.SaveChanges();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to delete an entry from the database. " + ex.Message + " " + ex.InnerException?.Message);
            }
        }
    }        
}
