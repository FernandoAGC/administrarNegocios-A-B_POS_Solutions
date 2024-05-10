using administrarNegocios_A_B_POS_Solutions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace administrarNegocios_A_B_POS_Solutions.Controllers;

public class LoginController : Controller
{
    private readonly AppDbContext _context;

    public LoginController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string correo, string contraseña, string tipoUsuario)
    {
        // Buscar el usuario en la base de datos de forma asíncrona
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo && u.Contraseña == contraseña);

        if (usuario != null)
        {
            // El usuario fue encontrado y se verifica si se loguea de acuerdo a su tipo de usuario
            // Obtener el tipo de usuario del usuario encontrado anteriormente
            var tipoUsuarioBD = await _context.TiposUsuario.FirstOrDefaultAsync(tu => tu.Id == usuario.TipoUsuarioId);

            if (tipoUsuarioBD?.Nombre == "Cliente" && tipoUsuarioBD.Nombre != tipoUsuario)
            {
                // Si un cliente se intenta loguear como administrador
                ModelState.AddModelError(string.Empty, "El cliente debe loguearse como cliente no como administrador.");
                // Vuelve a mostrar la vista de inicio de sesión con un mensaje de error
                return View();
            }
            // si es cliente y se loguea como cliente, se redirige a su respectiva vista
            // si es administrador se puede loguear como cliente o como administrador, se redirige a la vista segun el tipo de login
            return RedirectToAction("Inicio", tipoUsuario);
        }
        // El usuario no fue encontrado y muetra mensaje de error
        ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
        // Vuelve a mostrar la vista de inicio de sesión con un mensaje de error
        return View();
    }
}