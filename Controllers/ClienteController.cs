using administrarNegocios_A_B_POS_Solutions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace administrarNegocios_A_B_POS_Solutions.Controllers;

public class ClienteController : Controller
{
    private readonly AppDbContext _context;

    public ClienteController(AppDbContext context)
    {
        _context = context;
    }

    // Regresa la vista con la lista de los ultimos 20 items
    [HttpGet]
    public async Task<IActionResult> Inicio(string filtro, int usuarioId)
    {
        // Encontrar el negocioId asociado al usuarioId
        var negocioId = await _context.Negocios
            .Where(n => n.UsuarioId == usuarioId)
            .Select(n => n.Id)
            .FirstOrDefaultAsync();

        // Obtener los ítems relacionados con el negocioId encontrado
        var items = _context.Items.Include(i => i.CategoriaItem)
            .Include(i => i.Negocio)
            .Where(i => i.NegocioId == negocioId)
            .OrderByDescending(i => i.Id)
            .Take(20);

        // Filtrar por nombre de negocio si se proporciona una cadena de filtro
        if (!string.IsNullOrEmpty(filtro))
        {
            items = items.Where(i => i.Nombre.Contains(filtro) || i.CategoriaItem.Nombre.Contains(filtro));
        }
        // demanera asíncrona convertir los resultados en una lista
        return View(await items.ToListAsync());
    }

    public IActionResult Logout()
    {
        return RedirectToAction("Login", "Login");
    }

    // Funcionalidad para administrar Usuarios

    // Regresa la vista con la lista de todos los usuarios
    [HttpGet]
    public async Task<IActionResult> Items()
    {
        return View();
    }
}