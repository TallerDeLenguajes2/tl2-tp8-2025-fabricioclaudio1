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

    public IActionResult Index()
    {
        List<Presupuesto> presupuestos = _prep.Listar();
        return View(presupuestos);
    }

    public IActionResult BusquedaPorId()
    {
        return View();
    }
   
    public IActionResult Details(int id)
    {
        Presupuesto presupuestos = _prep.ObtenerSegunID(id);
        if (presupuestos == null)
        {
            ViewBag.Mensaje = "Presupuesto no encontrado.";
            return View();
        }
        return View(presupuestos);
    }





    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
