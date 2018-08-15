1.  Creamos un nuevo proyecto en src/data/Lanre.Data.Context para tener nuestra configuracion del context, mappings, migrations, ...

2.  Se añaden los nugets de:
    *   Microsoft.AspnetCore.Hosting
    *   Microsoft.EntityFrameworkCore
    *   Microsoft.EntityFrameworkCore.Design
    *   Microsoft.EntityFrameworkCore.InMemory (este será para el futuro)
    *   Microsoft.EntityFrameworkCore.Relational
    *   Microsoft.EntityFrameworkCore.SqlServer
    *   Microsoft.EntityFrameworkCore.Tools
  
3.  Se añaden las versiones en el dependencies.props

4.  Todo lo que veremos será explicado con más calma. (Formación de ef core con novedades)

5.  Se añade un ContextCore para que hereden todos los contextos
    *   Este contexto añadirá "AutoMagicamente" los Mappings necesarios
    *   Estos mappings estarán filtrados por una dataannotation para evitar los mappings que no sean de esta bdd

6.  Se añade una interfaz para nuestras dataannotations para los mappings

7.  Se añade una clase y una interfaz de la que hederadan los mappings

8.  Se añade una clase para registrar los contextos

// Hasta aquí las cosas "base"

9.  Se añade una dataannotation para nuestro contexto.

10. Se añade el contexto de SchedulerContext que contiene la entidad de Appointment del proyecto de src/Infrastructure/Lanre.Infrastructure.Entities

11. Se añade el mapping de appointment 

12. Se añade el seedData en el mapping

13. Se llama al registro desde el Startup.cs del proyecto de Host

14. Se añade la connection string en el appsettings.json

15. Se modifica el objeto de Settings para que tenga la connection string

16. Se guarda el commando para añadir migrations en un .ps1:
    *   dotnet ef migrations add $migration -c Lanre.Data.Context.Contexts.SchedulerContext -p ../../data/Lanre.Data.Context/Lanre.Data.Context.csproj -o Migrations/Schedule
    *   Hacemos el .ps1 parametizable
    *   Se ejecutará tal que: powershell ./add.migration.scheduler.ps1 -migration "Initial"

17. Se guarda otro para el update database
    *   dotnet ef database update -c Lanre.Data.Context.Contexts.SchedulerContext -p ../../data/Lanre.Data.Context/Lanre.Data.Context.csproj
    *   Hacemos el ps1 sin parametros
    *   powershell ./update.scheduler.ps1

18. Ejecutamos el ps1 de update y deberiamos ver la BDD con la información del seedData

19. Modificamos el controller base para que utilice el contexto de ef (de momento sin services ni repository)

20. Ejecutamos y miramos en swagger que el endpoint de appointments

### Futures 
*   Hacer un proyecto base que contenga la "Infrastructura" del proyecto de contexto.
