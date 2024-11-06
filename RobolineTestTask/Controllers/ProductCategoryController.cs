using Microsoft.AspNetCore.Mvc;
using RobolineTestTask.Database;
using System.Linq;

namespace RobolineTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly RobolineContext db;

        public ProductCategoryController(RobolineContext databaseContext)
        {
            db = databaseContext;
        }

        [HttpGet("categories/price")]
        public IActionResult GetPrice(int pageNumber, int pageSize)
        {
            try
            {
                int totalCount = db.ProductCategories.Count();

                var parameters = new PaginationParameters(pageNumber, pageSize, totalCount); // Внутри конструктора происходит валидация параметров
                                                                                             // В случае ошибки бросается ArgumentException

                // LINQ-выражение через методы расширений
                var result = db.ProductCategories
                    .Select(c => new
                    {
                        CategoryName = c.Name,
                        ProductCount = (c.Products != null) ? c.Products.Count() : 0,
                        AveragePrice = (c.Products != null) ? Math.Round(c.Products.Average(p => (double)p.Price), 2) : 0
                    })
                    .Skip((parameters.PageNumber-1)*parameters.PageSize)
                    .Take(parameters.PageSize);

                var response = new
                {
                    Data = result,                       // данные
                    PageNumber = parameters.PageNumber,  // номер страницы
                    TotalPages = parameters.TotalPages,  // общее кол-во страниц
                    PageSize = parameters.PageSize,      // размер страницы
                    TotalCount = totalCount              // кол-во всех записей
                };

                return Ok(response);
            }
            catch (ArgumentException ex) // ошибка валидации параметров
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to read records from the database. " + ex.Message + " " + ex.InnerException?.Message);
            }
        }

        // получение всех категорий
        [HttpGet("categories")]
        public IActionResult Read()
        {
            try
            {
                var categories = db.ProductCategories.ToList();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to read records from the database. " + ex.Message + " " + ex.InnerException?.Message);
            }
        }

        // получение категории по id
        [HttpGet("categories/{id}")]
        public IActionResult Read(int id)
        {
            try
            {
                var category = db.ProductCategories.FirstOrDefault(u => u.Id == id);

                if (category == null) 
                    return NotFound("The database entry doesn't exist");

                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to read the record from the database. " + ex.Message + " " + ex.InnerException?.Message);
            }

        }

        // добавление новой категории
        [HttpPost("categories")]
        public IActionResult Create(ProductCategory category)
        {
            try
            {
                if (category != null)
                {
                    db.ProductCategories.Add(category);
                    db.SaveChanges();
                    return Ok(category);
                }
                return BadRequest("Product Category is NULL");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to add an entry to the database. " + ex.Message + " " + ex.InnerException?.Message);
            }
        }

        // обновление категории
        [HttpPut("categories/{id}")]
        public IActionResult Update(int id, ProductCategory category) 
        {
            try
            {
                if (category != null)
                {
                    if (category.Id == id)
                    {
                        db.ProductCategories.Update(category);
                        db.SaveChanges();

                        return Ok(category);
                    }
                    return BadRequest("The product category ID in the request body doesn't match the ID in the request URL");
                }
                return BadRequest("Product category is NULL");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to update an entry to the database. " + ex.Message + " " + ex.InnerException?.Message);
            }
        }

        // удаление категории
        [HttpDelete("categories/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var category = db.ProductCategories.FirstOrDefault(u => u.Id == id);

                if (category == null) 
                    return NotFound("The database entry doesn't exist");

                db.ProductCategories.Remove(category);
                db.SaveChanges();

                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to delete an entry from the database. " + ex.Message + " " + ex.InnerException?.Message);
            }
        }
    }
}
