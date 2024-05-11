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

    // Funcionalidad para administrar Items
    [HttpGet]
    public async Task<IActionResult> Items(int negocioId)
    {
        // Obtener los ítems relacionados con el negocioId encontrado
        return View(await _context.Items.Include(i => i.CategoriaItem).Include(i => i.Negocio).Where(i => i.NegocioId == negocioId).ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AgregarOActualizarItem(Item item)
    {
        if (item.Id.ToString() == null || item.Id.ToString() == "" || item.Id.Equals(null))
        {
            await _context.Items.AddAsync(item);
            
        } else {
            _context.Items.Update(item);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction("Items", "Cliente", new { negocioId = item?.NegocioId });
    }

    [HttpPost]
    public async Task<IActionResult> EliminarItem(int id)
    {
        var item = await _context.Items.FindAsync(id);
        // Buscar el item a eliminar en la base de datos
        if (item != null)
        {
            // Eliminar el item de la base de datos
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Items", "Cliente", new { negocioId = item?.NegocioId });
    }

    // obtiene los datos del item a editar
    [HttpGet]
    public async Task<IActionResult> ObtenerItem(int id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item != null)
        {
            return Json(new
            {
                id = item.Id,
                nombre = item.Nombre,
                descripcion = item.Descripcion,
                categoriaItemId = item.CategoriaItemId,
                precio = item.Precio,
                negocioId = item.NegocioId
            });
        }
        return NotFound();
    }
}