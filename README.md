# Movie-Management-Application
API para la gestión de películas desarrollada en **.NET 8**, siguiendo una arquitectura en capas.  
Permite administrar películas, sincronizar información desde una API externa y exponer endpoints protegidos con JWT.

## Cómo ejecutar el proyecto

### Requisitos previos

- Visual Studio 2022
- .NET 8 SDK instalado
- SQL Server

### Pasos para la ejecución

1. Clonar el repositorio y navegar al directorio del proyecto:
2. En la carpeta MovieManagementApp, alli abrir la solución MovieManagementSolution.sln con Visual Studio.
3. Configurar la cadena de conexión a SQL Server en MovieManagementApp/appsettings.json:
4. Crear la base de datos con Entity Framework Migrations.
    En la ruta que termina en Movie-Management-Application ejecutar:
   ```bashen
    dotnet ef migrations add InitialCreate -p Repository -s MovieManagementApp
    dotnet ef database update -p Repository -s MovieManagementApp
6. Ejecutar la aplicación presionando F5 o haciendo clic en el botón de Iniciar.
