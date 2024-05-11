# AdministrarNegocios-A&B_POS_Solutions
Proyecto es desarrollado como solucion a una prueba tecnica para la empresa A&B POS Solutions.

# Desarrollo | Instrucciones
Desarrollado por: Fernando Amilcar Galicia Cáceres.

Tecnologias: .NET 8.0, Entity Framework Core, Identity, entre otros.

<br>

Al proyecto se añadieron los paquetes:
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools

<br>

Se configuro Idenity:
dotnet aspnet-codegenerator identity -dc AppDbContext
**Se utilizo la herramienta aspnet CodeGenerator:
dotnet tool install -g dotnet-aspnet-codegenerator

# Asignar cadena de conexión a su Base de Datos
En el archivo "appsettings.json" remplazar el valor de la clave "DefaultConnection" por su propia cadeneda de conexión a su BD.
**("ConnectionStrings": {"DefaultConnection": "cadena de conexión"}) 

# Realizar Migraciones (Code First)
dotnet ef migrations add NombredelaMigracion
dotnet ef database update

# Ejecutar
dotnet run

# Ingresar | Login
Las migraciones incluyen registros de datos inciciales, algunas credenciales de usuarios para poder iniciar sesión son:
Administradores:
Correo = "lg@gmail.com", Contraseña = "lg123" | Correo = "jp@gmail.com", Contraseña = "jp123"

<br>

Clientes:
Correo = "pg@gmail.com", Contraseña = "pg123"

<br>

**Los demas registros se pueden ver definidos en el archivo AppDbContext.cs o consultando la base de datos despues de hacer las migraciones 

<br>
<br>

# AdministrarNegocios-A&B_POS_Solutions
Project is developed as a solution to a technical test for the company A&B POS Solutions.

# Development | Instructions
Developed by: Fernando Amilcar Galicia Cáceres.

Technologies: .NET 8.0, Entity Framework Core, Identity, among others.

<br>

The following packages were added to the project:
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools

<br>

Idenity was configured:
dotnet aspnet-codegenerator identity -dc AppDbContext
**The aspnet CodeGenerator tool was used:
dotnet tool install -g dotnet-aspnet-codegenerator

# Assign connection string to your Database
In the "appsettings.json" file, replace the value of the "DefaultConnection" key with your own connection string to your database.
**("ConnectionStrings": {"DefaultConnection": "connection string"})

# Make Migrations (Code First)
dotnet ef migrations add MigrationName
dotnet ef database update
