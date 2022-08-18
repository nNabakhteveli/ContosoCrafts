using Microsoft.AspNetCore.Mvc;

using ContosoCrafts.Models;
using ContosoCrafts.Services;

namespace ContosoCrafts.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    public JsonFileProductService ProductService { get; }
    
    public ProductsController(JsonFileProductService productService)
    {
        this.ProductService = productService;
    }

    [HttpGet]
    public IEnumerable<Product> Get()
    {
        return ProductService.GetProducts();
    }
}