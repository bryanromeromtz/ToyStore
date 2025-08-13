# ToyStore Application

Monorepo con:

- `client/` → React + Vite + Bootstrap (frontend)
- `server/` → ASP.NET Core Web API + EF Core + SQL Server Express (backend)

## Requisitos

- **.NET SDK 8.0+**
- **Node.js 18+** y **npm**
- **Git**

## Cómo ejecutar

### Client
cd client/toy-store-client
npm install
npm run dev

## Server
### (instrucciones de .NET, migraciones, etc.)

### Ir a la carpeta del servidor
cd server/ToyStore

### Restaurar dependencias .NET
dotnet restore

### Aplicar migraciones de base de datos
dotnet ef database update

### Ejecutar el servidor
dotnet run
