using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp8_2025_fabricioclaudio1.Models;

namespace tl2_tp8_2025_fabricioclaudio1.Controllers;

public class PresupuestoController : Controller
{
    private readonly PresupuestoRepository _prep;

    public PresupuestoController()
    {
        _prep = new PresupuestoRepository();
    }

    //Metodos
    public IActionResult List()
    {
        List<Presupuesto> presupuestos = _prep.Listar();
        return View(presupuestos);
    }

    public IActionResult Details(int id)
    {
        Presupuesto? presupuesto = _prep.ObtenerSegunID(id);
        if (presupuesto == null)
        {
            ViewBag.Mensaje = "Presupuesto no encontrado.";
            return View();
        }
        return View(presupuesto);
    }

    [HttpGet]
    public IActionResult Create()
    {
        Presupuesto presupuesto = new Presupuesto { IdPresupuesto = 0 };
        return View("Form", presupuesto);
    }

    [HttpPost]
    public IActionResult Create(Presupuesto presupuesto)
    {
        if (!ModelState.IsValid)
            return View("Form", presupuesto);

        _prep.Crear(presupuesto);

        return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var presupuesto = _prep.ObtenerSegunID(id);
        if (presupuesto == null)
            return NotFound();
        return View("Form", presupuesto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Presupuesto presupuesto)
    {
        if (!ModelState.IsValid)
            return View(presupuesto);
        
        // Opcional: verificar que exista
        var existing = _prep.ObtenerSegunID(presupuesto.IdPresupuesto);
        if (existing == null) return NotFound();

        _prep.Modificar(existing.IdPresupuesto, presupuesto);

        return RedirectToAction("List");
    }
    //
    [HttpPost]
    public IActionResult Delete(int id)
    {
        _prep.EliminarID(id);

        return RedirectToAction("List");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
