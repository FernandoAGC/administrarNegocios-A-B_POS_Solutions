using administrarNegocios_A_B_POS_Solutions.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// definicion de rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}"
);
app.MapControllerRoute(
    name: "Administrador",
    pattern: "{controller=Administrador}/{action=Inicio}"
);
app.MapControllerRoute(
    name: "Administrador",
    pattern: "{controller=Administrador}/{action=Usuarios}"
);
app.MapControllerRoute(
    name: "Administrador",
    pattern: "{controller=Administrador}/{action=Negocios}"
);
app.MapControllerRoute(
    name: "Cliente",
    pattern: "{controller=Cliente}/{action=Inicio}"
);
app.MapControllerRoute(
    name: "Cliente",
    pattern: "{controller=Cliente}/{action=Items}"
);

app.Run();
