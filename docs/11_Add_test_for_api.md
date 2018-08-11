1.  Se añade un nuevo proyecto de tipo Xunit
2.  Añadimos en el dependency.props las versiones de los paquetes, tooling, ...
3.  Se añade un Startup que solo cargue la configuración de la Api
4.  Se añade un ServerFactory que nos devuelva el test server vinculado a ese Startup
5.  Se añade un test para Values Controller
6.  Se extrae la funcionalidad a una base
7.  Se añaden los test para Appointment controller y se van añadiendo metodos a la base
    *   Se habilitan los live test
8.  Se añaden los test para las fluent validations