using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;

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

    [Route("Rate")]
    [HttpGet]
    public ActionResult Get([FromQuery] string productId, [FromQuery] int rating)
    {
        this.AddRating(productId, rating);
        return Ok();
    }

    public void AddRating(string productId, int rating)
    {
        var products = ProductService.GetProducts();
        var query = products.First(x => x.Id == productId);

        if (query.Ratings == null)
        {
            query.Ratings = new int[] { rating };
        }
        else
        {
            var productRating = query.Ratings.ToList();
            productRating.Add(rating);
            query.Ratings = productRating.ToArray();
        }

        string pathToJsonFile = @"/Users/nika/Desktop/dotnet/ContosoCrafts/ContosoCrafts/wwwroot/data/products.json";
        using (var outputStream = System.IO.File.OpenWrite(pathToJsonFile))
        {
            JsonSerializer.Serialize<IEnumerable<Product>>(
                new Utf8JsonWriter(outputStream, new JsonWriterOptions()
                {
                    SkipValidation = true,
                    Indented = true
                }),
                products
            );
        }
    }
}