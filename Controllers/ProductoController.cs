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
    public IActionResult List()
    {
        List<Producto> productos = _prod.Listar();
        return View(productos);
    }

    [HttpGet]
    public IActionResult Create()
    {
        Producto producto = new Producto{IdProducto = 0};
        return View("Form", producto);
    }

    [HttpPost]
    public IActionResult Create(Producto producto)
    {
        if (!ModelState.IsValid)
            return View("Form", producto);

        _prod.Crear(producto);

        return RedirectToAction("List"); 
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var producto = _prod.ObtenerID(id);
        if (producto == null)
            return NotFound();
        return View("Form",producto);
    }

    // POST: recibe el modelo modificado y lo persiste
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Producto producto)
    {
        if (!ModelState.IsValid)
            return View(producto);

        // Opcional: verificar que exista
        var existing = _prod.ObtenerID(producto.IdProducto);
        if (existing == null) return NotFound();

        // Actualizar campos (o usar _context.Update(producto) con cuidado)
        _prod.Modificar(existing.IdProducto, producto);

        return RedirectToAction("List");
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        _prod.EliminarID(id);

        return RedirectToAction("List");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
