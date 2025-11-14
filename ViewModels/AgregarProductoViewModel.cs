using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.ViewModels;
public class AgregarProductoViewModel
{
    [Required]
    [Range(1, 999)]
    public int Cantidad { get; set; }

    public SelectList? ListaProductos { get; set; }
}