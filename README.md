# 🏰 Disney World REST API - Alkemy .NET Challenge

![.NET 5](https://img.shields.io/badge/.NET-5.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC292B?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework_Core-5.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-Auth-000000?style=for-the-badge&logo=json-web-tokens&logoColor=white)

API RESTful desarrollada en **.NET 5** y **Entity Framework Core** con **SQL Server** como parte del **Alkemy C# Acceleration Challenge**. Permite explorar, filtrar y administrar el universo de películas y personajes del mundo de Disney.

---

## 📚 Índice de Endpoints

### 1. Autenticación (`/api/auth`)
- `POST /api/auth/register`: Registra un nuevo usuario y envía un correo electrónico de bienvenida.
- `POST /api/auth/login`: Valida credenciales y genera un token JWT de sesión.

### 2. Personajes (`/api/characters`)
- `GET /api/characters`: Listado simplificado (imagen y nombre). Soporta filtros:
  - `?name={nombre}` (Búsqueda por nombre)
  - `?age={edad}` (Filtro por edad)
  - `?movies={idMovie}` (Filtro por película asociada)
- `GET /api/characters/{id}`: Detalle completo del personaje con historia, peso, edad y películas asociadas.
- `POST /api/characters`: Crea un nuevo personaje asociándolo a películas existentes.
- `PUT /api/characters`: Actualiza la información del personaje.
- `DELETE /api/characters/{id}`: Elimina un personaje.

### 3. Películas o Series (`/api/movies`)
- `GET /api/movies`: Listado simplificado (imagen, título, fecha). Soporta filtros:
  - `?name={titulo}` (Búsqueda por título)
  - `?genre={idGenre}` (Filtro por género)
  - `?order=ASC|DESC` (Ordenamiento por fecha de creación)
- `GET /api/movies/{id}`: Detalle completo con género, calificación (1-5) y elenco de personajes.
- `POST /api/movies`: Crea una nueva película o serie.
- `PUT /api/movies`: Modifica una película y sus participaciones.
- `DELETE /api/movies/{id}`: Elimina una película.

### 4. Géneros (`/api/genres`)
- `GET /api/genres`: Listado de todos los géneros registrados.
- `POST /api/genres`: Crea un nuevo género.

---

## 🚀 Instalación y Puesta en Marcha

### Prerrequisitos
- [.NET 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
- SQL Server LocalDB o SQL Server Express
- Visual Studio 2019 / 2022 o VS Code

### 1. Clonar el repositorio
```bash
git clone https://github.com/LucasFerrenti/DisneyMovies-Alkemy.git
cd DisneyMovies-Alkemy
```

### 2. Configurar la cadena de conexión
Edita el archivo `appsettings.json` con tus credenciales locales de SQL Server:

```json
{
  "ConnectionStrings": {
    "DisneyMoviesDB": "Server=(localdb)\\mssqllocaldb;Database=DisneyMoviesDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "AppSettings": {
    "JwtKey": "TuClaveSecretaSuperSeguraParaJWT123456!",
    "Email": "tu_email@ejemplo.com",
    "Password": "tu_password_smtp"
  }
}
```

### 3. Aplicar Migraciones de Base de Datos
```bash
dotnet ef database update
```

### 4. Ejecutar el Servidor
```bash
dotnet run
```

La API iniciará en `https://localhost:5001` o `http://localhost:5000`.

---

## 📄 Licencia

Este proyecto fue desarrollado con propósitos educativos para el programa de aceleración de Alkemy.
