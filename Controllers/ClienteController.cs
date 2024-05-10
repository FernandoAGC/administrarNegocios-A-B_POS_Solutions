using administrarNegocios_A_B_POS_Solutions.Models;
using Microsoft.AspNetCore.Mvc;

namespace administrarNegocios_A_B_POS_Solutions.Controllers;

public class ClienteController : Controller
{
    private readonly AppDbContext _context;

    public ClienteController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Home()
    {
        return View();
    }
}