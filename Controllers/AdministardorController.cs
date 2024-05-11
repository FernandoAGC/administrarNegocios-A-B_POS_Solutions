using System.Diagnostics;
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

    // Regresa la vista con la lista de los ultimos 20 negocios
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

    public IActionResult Logout()
    {
        return RedirectToAction("Login", "Login");
    }

    // Funcionalidad para administrar Usuarios

    // Regresa la vista con la lista de todos los usuarios
    [HttpGet]
    public async Task<IActionResult> Usuarios()
    {
        // Obtener todos los usuarios y tipos de usuario de la base de datos para construir un modelo para la vista
        var viewModel = new UsuariosTiposViewModel
        {
            Usuarios = await _context.Usuarios.Include(u => u.TipoUsuario).ToListAsync(),
            TiposUsuario = await _context.TiposUsuario.ToListAsync()
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AgregarOActualizarUsuario(Usuario usuario)
    {
        if (usuario.Id.ToString() == null || usuario.Id.ToString() == "" || usuario.Id.Equals(null))
        {
            await _context.Usuarios.AddAsync(usuario);
            
        } else {
            _context.Usuarios.Update(usuario);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction("Usuarios", "Administrador");
    }

    [HttpPost]
    public async Task<IActionResult> EliminarUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        // Buscar el usuario a eliminar en la base de datos
        if (usuario != null)
        {
            // Eliminar el usuario de la base de datos
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Usuarios", "Administrador");
    }

    // obtiene los datos del usuario a editar
    [HttpGet]
    public async Task<IActionResult> ObtenerUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            return Json(new
            {
                id = usuario.Id,
                nombres = usuario.Nombres,
                apellidos = usuario.Apellidos,
                correo = usuario.Correo,
                tipoUsuarioId = usuario.TipoUsuarioId
            });
        }
        return NotFound();
    }

    // Funcionalidad para administrar Negocios
    [HttpGet]
    public async Task<IActionResult> Negocios()
    {
        return View(await _context.Negocios.Include(n => n.Usuario).ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AgregarOActualizarNegocio(Negocio negocio)
    {
        if (negocio.Id.ToString() == null || negocio.Id.ToString() == "" || negocio.Id.Equals(null))
        {
            await _context.Negocios.AddAsync(negocio);
            
        } else {
            _context.Negocios.Update(negocio);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction("Negocios", "Administrador");
    }

    [HttpPost]
    public async Task<IActionResult> EliminarNegocio(int id)
    {
        var negocio = await _context.Negocios.FindAsync(id);
        // Buscar el usuario a eliminar en la base de datos
        if (negocio != null)
        {
            // Eliminar el usuario de la base de datos
            _context.Negocios.Remove(negocio);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Negocios", "Administrador");
    }

    // obtiene los datos del negocio a editar
    [HttpGet]
    public async Task<IActionResult> ObtenerNegocio(int id)
    {
        var negocio = await _context.Negocios.FindAsync(id);
        if (negocio != null)
        {
            return Json(new
            {
                id = negocio.Id,
                nombre = negocio.Nombre,
                descripcion = negocio.Descripcion,
                fechaCreacion = negocio.FechaCreacion,
                usuarioId = negocio.UsuarioId
            });
        }
        return NotFound();
    }
}