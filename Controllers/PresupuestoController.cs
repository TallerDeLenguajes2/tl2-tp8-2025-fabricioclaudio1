using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MVC.Models;
using MVC.Repositories;
using MVC.ViewModels;

namespace MVC.Controllers;

public class PresupuestoController : Controller
{
    private readonly PresupuestoRepository _prep;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public PresupuestoController(PresupuestoRepository prep ,IHttpContextAccessor httpContextAccessor)
    {
        _prep = prep;
        _httpContextAccessor = httpContextAccessor;
    }

    //Metodos
    public IActionResult List()
    {
        List<Presupuesto> presupuestos = _prep.Listar();

        List<PresupuestoViewModel> presupuestosVM = new List<PresupuestoViewModel>();

        foreach (var pre in presupuestos)
        {
            PresupuestoViewModel presupuestoVM = new PresupuestoViewModel
            {
                IdPresupuesto = pre.IdPresupuesto,
                NombreDestinario = pre.NombreDestinario,
                ListaDetalle = pre.ListaDetalle,
            };



            presupuestosVM.Add(presupuestoVM);
        }

        return View(presupuestosVM);
    }

    public IActionResult Details(int id)
    {
        Presupuesto? presupuesto = _prep.ObtenerSegunID(id);
        if (presupuesto == null)
        {
            ViewBag.Mensaje = "Presupuesto no encontrado.";
            return View();
        }

        PresupuestoViewModel presupuestoVM = new PresupuestoViewModel
        {
            IdPresupuesto = presupuesto.IdPresupuesto,
            NombreDestinario = presupuesto.NombreDestinario,
            ListaDetalle = presupuesto.ListaDetalle,
        };

        return View(presupuestoVM);
    }

    [HttpGet]
    public IActionResult Create()
    {
        PresupuestoViewModel presupuestoVM = new PresupuestoViewModel { IdPresupuesto = 0 };
        return View("Form", presupuestoVM);
    }

    [HttpPost]
    public IActionResult Create(PresupuestoViewModel presupuestoVM)
    {
        if (!ModelState.IsValid)
        {
            return View("Form", presupuestoVM);
        }
        var nuevo = new Presupuesto
        {
            NombreDestinario = presupuestoVM.NombreDestinario
        };

        _prep.Crear(nuevo);

        return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var presupuesto = _prep.ObtenerSegunID(id);
        if (presupuesto == null)
        {
            return NotFound();
        }

        PresupuestoViewModel presupuestoVM = new PresupuestoViewModel
        {
            IdPresupuesto = presupuesto.IdPresupuesto,
            NombreDestinario = presupuesto.NombreDestinario,
            ListaDetalle = presupuesto.ListaDetalle,
        };

        return View("Form", presupuestoVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(PresupuestoViewModel presupuestoVM)
    {
        if (!ModelState.IsValid)
        {
            return View(presupuestoVM);
        }

        // Opcional: verificar que exista
        var existing = _prep.ObtenerSegunID(presupuestoVM.IdPresupuesto);
        if (existing == null) return NotFound();

        //Modifico obj existente
        existing.NombreDestinario = presupuestoVM.NombreDestinario;

        _prep.Modificar(existing.IdPresupuesto, existing);

        return RedirectToAction("List");
    }
    //
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var presupuesto = _prep.ObtenerSegunID(id);
        if (presupuesto == null)
        {
            return NotFound();
        }

        _prep.EliminarID(id);

        return RedirectToAction("List");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
