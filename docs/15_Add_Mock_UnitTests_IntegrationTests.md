1.  Los tests están rotos ahora se tienen que arreglar y "especializar"
2.  Dividir el proyecto de Tests en 3
    *   Test.Core para las bases de las configuraciones de los tests
    *   Unit Tests
    *   Integration Tests
3.  Los nugets que necesitaremos(o no) serán los de:
    *   Microsoft.AspNetCore.TestHost
    *   Microsoft.NET.Test.Sdk
    *   Moq
    *   Moq.EntityFrameworkCore
    *   xunit
    *   xunit.runner.visualstudio
4.  Creamos un startup para los tests que registre los middlewares del proyecto de Api
5.  Registramos la bdd como in memory
6.  Un server factory para instanciar el test server para los unit tests de integración
7.  Creamos los mocks que necesitamos para instanciar los controlladores
8.  Creamos una base para los tests de la Api. Esta base genera:
    *   los mocks
    *   la instancia del controllador que queremos testear
    *   tiene una función para hacer un override y tener datos básicos
9.  Añadimos los tests de los controllers (con new)
10. Añadimos los tests de las fluent validations
11. Añadimos un fix en el proyecto de Api                
    *   .AddMvcCore().AddApplicationPart(typeof(Controllers.V1.AppointmentsController).Assembly) // Fix for integration tests
12. Añadimos otro fix con el directory root del proyecto de Api
    *   webHost.UseContentRoot("DIRECTORIO")
13. Añadimos los tests de integración (con el test server)