using administrarNegocios_A_B_POS_Solutions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace administrarNegocios_A_B_POS_Solutions.Controllers;

public class AdministradorController : Controller
{
    private readonly AppDbContext _context;

    public AdministradorController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Inicio(string filtro)
    {
        // Obtener los ultimos 20 negocios
        var negocios = _context.Negocios.Include(n => n.Usuario) // Incluye los datos del usuario asociado al negocio
            .OrderByDescending(n => n.Id) // Ordena por ID de manera descendente
            .Take(20); // Limita a 20 resultados

        // Filtrar por nombre de negocio si se proporciona una cadena de filtro
        if (!string.IsNullOrEmpty(filtro))
        {
            negocios = negocios.Where(n => n.Nombre.Contains(filtro));
        }
        // demanera asíncrona convertir los resultados en una lista
        return View(await negocios.ToListAsync());
    }

    public IActionResult Usuarios()
    {
        return View();
    }

    public IActionResult Negocios()
    {
        return View();
    }

    public IActionResult Logout()
    {
        return RedirectToAction("Login", "Login");
    }
}