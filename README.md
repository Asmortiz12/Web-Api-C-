# Web-Api

![image](https://github.com/user-attachments/assets/c3545418-debc-4b0b-a841-c524abece7e8)

Este proyecto es una API web construida con ASP.NET Core. A continuación se detallan los pasos para instalar y levantar el proyecto.

## Requisitos previos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

## Instalación

1. Clona el repositorio en tu máquina local:
    ```sh
    git clone https://github.com/tu-usuario/Web-Api.git
    ```

2. Navega al directorio del proyecto:
    ```sh
    cd Web-Api/Web-Api
    ```

3. Restaura las dependencias del proyecto:
    ```sh
    dotnet restore
    ```

4. Configura la cadena de conexión a la base de datos en el archivo `appsettings.json`:
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Database=tu_base_de_datos;Username=tu_usuario;Password=tu_contraseña"
      }
    }
    ```

5. Aplica las migraciones a la base de datos:
    ```sh
    dotnet ef database update
    ```

## Levantar el proyecto

1. Compila y ejecuta la aplicación:
    ```sh
    dotnet run
    ```

2. La API estará disponible en `http://localhost:5151/swagger/index.html`.

## Uso

Puedes probar los endpoints de la API utilizando herramientas como [Postman](https://www.postman.com/) o [curl](https://curl.se/).

## Contribuir

Si deseas contribuir a este proyecto, por favor sigue los siguientes pasos:

1. Haz un fork del repositorio.
2. Crea una nueva rama (`git checkout -b feature/nueva-funcionalidad`).
3. Realiza tus cambios y haz commit (`git commit -am 'Añadir nueva funcionalidad'`).
4. Sube tus cambios a tu fork (`git push origin feature/nueva-funcionalidad`).
5. Abre un Pull Request.

## Licencia

Este proyecto está licenciado bajo la Licencia MIT. Consulta el archivo [LICENSE](LICENSE) para más detalles.
