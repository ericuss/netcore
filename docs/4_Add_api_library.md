1.  Se añade una "class library" de tipo net standard en /src/clients llamada Lanre.Clients.Api
2.  Se modifica el .csproj para que pille la versión del config
3.  Se añade un fichero de configuracion del proyecto y los middlewares que necesita
    *   .AddMvcCore()
    *   .AddJsonFormatters()
    *   .AddApiExplorer()
    *   .UseMvc()
4.  Se añaden los nugets necesarios
    *   Microsoft.AspNetCore.Http.Abstractions
    *   Microsoft.AspNetCore.Mvc.ApiExplorer
    *   Microsoft.AspNetCore.Mvc.Core
    *   Microsoft.AspNetCore.Mvc.Formatters.Json
    *   Microsoft.Extensions.DependencyInjection
5.  Se añade un controller de ejemplo
    * Hay que añadir una data annotation por el controller y especificar los verbos de las funciones
6.  Se añade un base controller de momento sin nada
7.  Se añade otro con las operaciones básicas de una api
8.  Se añade un ejemplo de controller con la Api añadiendo un proyecto de entidades