using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp8_2025_fabricioclaudio1.Models;

namespace tl2_tp8_2025_fabricioclaudio1.Controllers;

public class ProductoController : Controller
{
    private readonly ProductoRepository _prod;

    public ProductoController()
    {
        _prod = new ProductoRepository();
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Producto> productos = _prod.Listar();
        return View(productos);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
