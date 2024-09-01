using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPIBasic.Data;
using WebAPIBasic.Filters;

namespace WebAPIBasic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
		private readonly ILogger<ProductController> logger;

		public ProductController(ApplicationDbContext context, ILogger<ProductController> logger)
        {
            _context = context;
			this.logger = logger;
		}

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<Products>> Get()
        {
            var userName = User.Identity.Name;
            var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var records = _context.Set<Products>().ToList();
            return Ok(records);
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        [LogSensitiveAction]
        public ActionResult<Products> Get(int id)
        {
            this.logger.LogDebug("Getting product #" + id);
            var records = _context.Set<Products>().Find(id);
            if (records == null)
            {
                this.logger.LogWarning("product #{id} this is not found -- time: {y}", id, DateTime.Now);
            }
            return records == null ? NotFound() : Ok(records);
        }
        // public DbSet<Product> products { get; set; }
        [HttpPost]
        [Route("")]
        public ActionResult<int> CreateProduct(Products product)
        {
            product.ProductId = 0;
            _context.Set<Products>().Add(product);
            _context.SaveChanges();
            return Ok(product.ProductId);
        }

        [HttpPut]
        [Route("")]
        public ActionResult UpdateProduct(Products product)
        {
            var exsitingProduct = _context.Set<Products>().Find(product.ProductId);
            exsitingProduct.ProductName = product.ProductName;
            exsitingProduct.Sku = product.Sku;
            _context.Set<Products>().Update(exsitingProduct);
            _context.SaveChanges(true);
            return Ok();
        }

        [HttpDelete]
        [Route("{productId}")]
        public ActionResult DeleteProduct(int productId)
        {
            var existingProduct = _context.Set<Products>().Find(productId);
            _context.Set<Products>().Remove(existingProduct);
            _context.SaveChanges();
            return Ok();
        }
    }
}
