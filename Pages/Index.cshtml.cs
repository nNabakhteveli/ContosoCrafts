using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ContosoCrafts.Services;
using ContosoCrafts.Models;


namespace ContosoCrafts.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private JsonFileProductService ProductService;
    public IEnumerable<Product> Products { get; private set; }

    public IndexModel(ILogger<IndexModel> logger, JsonFileProductService jsonFileProductService)
    {
        _logger = logger;
        ProductService = jsonFileProductService;
    }

    public void OnGet()
    {
        Products = ProductService.GetProducts();
    }
}