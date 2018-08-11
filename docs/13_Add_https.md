1.  En la aplicación de host añadimos una clase extensora
    *   Se añade la redirección de http a https
2.  Se añade el puerto en el appsettings
3.  Ejecutamos el comando para generar un certificado
    *   dotnet dev-certs https -t
4.  se añade hsts