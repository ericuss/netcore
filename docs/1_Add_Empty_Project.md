1.  Estructura de directorios y ficheros.

-   Tendremos el .sln en la raíz y en /src los proyectos

-   Dentro de src tendremos diferentes:

    -   clients para los proyectos de apis, mvc, consola, …

    -   data para lo referente a BDD, repositorios, …

    -   domain/business dependiendo de la arquitectura que se haya definido

    -   configuration para todo lo referente a la configuración de los proyectos

    -   …

-   Tendremos una carpeta de /docs para toda la documentación del proyecto

1.  Creamos un proyecto web vacio en /src/clients/Lanre.Clients.Host

    -   Lanre será nuestro nombre de proyecto

    -   Se llama host porque será el que lance nuestra aplicación y tendrá las
        referencias pertinentes a los demás proyectos

    -   Sobretodo nos tenemos que fijar en que no esté el AspnetCore.All

2.  Añadimos en sln en la raíz.
