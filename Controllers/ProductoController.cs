using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Repositories;
using MVC.Models;
using MVC.ViewModels;


namespace MVC.Controllers;

public class ProductoController : Controller
{
    private readonly ProductoRepository _prod;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProductoController(ProductoRepository prod, IHttpContextAccessor httpContextAccessor)
    {
        _prod = prod;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    public IActionResult List()
    {
        List<Producto> productos = _prod.Listar();
        List<ProductoViewModel> productosVM = new List<ProductoViewModel>();

        foreach (var item in productos)
        {
            ProductoViewModel productoVM = new ProductoViewModel
            {
                IdProducto = item.IdProducto,
                Descripcion = item.Descripcion,
                Precio = item.Precio,
            };

            productosVM.Add(productoVM);
        }
        return View(productosVM);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ProductoViewModel productoVM = new ProductoViewModel { IdProducto = 0 };
        return View("Form", productoVM);
    }

    [HttpPost]
    public IActionResult Create(ProductoViewModel productoVM)
    {
        if (!ModelState.IsValid)
        {
            return View("Form", productoVM);
        }

        var nuevo = new Producto
        {
            Descripcion = productoVM.Descripcion,
            Precio = productoVM.Precio
        };

        _prod.Crear(nuevo);

        return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var producto = _prod.ObtenerID(id);
        if (producto == null)
        {
            return NotFound();
        }

        ProductoViewModel productoVM = new ProductoViewModel
        {
            IdProducto = producto.IdProducto,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio,
        };
        return View("Form", productoVM);
    }

    // POST: recibe el modelo modificado y lo persiste
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(ProductoViewModel productoVM)
    {
        if (!ModelState.IsValid)
        {
            return View(productoVM);
        }

        // Opcional: verificar que exista
        var existing = _prod.ObtenerID(productoVM.IdProducto);
        if (existing == null) return NotFound();

        //Modifico obj existente
        existing.Descripcion = productoVM.Descripcion;
        existing.Precio = productoVM.Precio;

        // Actualizar campos (o usar _context.Update(producto) con cuidado)
        _prod.Modificar(existing.IdProducto, existing);

        return RedirectToAction("List");
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var producto = _prod.ObtenerID(id);
        if (producto == null)
        {
            return NotFound();
        }

        _prod.EliminarID(id);

        return RedirectToAction("List");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
