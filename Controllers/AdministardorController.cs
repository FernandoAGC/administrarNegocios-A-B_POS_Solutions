using administrarNegocios_A_B_POS_Solutions.Models;
using Microsoft.AspNetCore.Mvc;

namespace administrarNegocios_A_B_POS_Solutions.Controllers;

public class AdministradorController : Controller
{
    private readonly AppDbContext _context;

    public AdministradorController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Inicio()
    {
        return View();
    }
}