# Mi Primera Web API - Gestión Empresarial

## Descripción del proyecto

Este proyecto consiste en el desarrollo de una Web API utilizando ASP.NET Core 8 y C#, que permite administrar información empresarial mediante operaciones CRUD (Create, Read, Update y Delete).

La API utiliza Entity Framework Core para interactuar con una base de datos MySQL denominada `db_empresa`.

El proyecto fue desarrollado como parte de una actividad académica de programación, con el objetivo de implementar controladores y endpoints HTTP.

## Autor

Gabriela Elízabeth Noemí Aguilar Vásquez | Universidad Mariano Gálvez |Ingeniería en Sistemas de Información

## Video demostrativo

https://youtu.be/UhMwl7ep4sY

## Tecnologías utilizadas

- C#
- ASP.NET Core 8
- Entity Framework Core
- Pomelo.EntityFrameworkCore.MySql
- MySQL
- MySQL Workbench
- Swagger
- Visual Studio 2022

## Estructura del proyecto

El sistema cuenta con tres módulos principales:

### Clientes
Permite consultar, registrar, actualizar y eliminar clientes.

### Empleados
Permite administrar los registros de empleados y sus puestos de trabajo.

### Puestos
Permite administrar los diferentes puestos disponibles dentro de la empresa.

## Operaciones CRUD

| Método | Operación | Descripción |
|---|---|---|
| GET | SELECT | Consultar registros |
| POST | INSERT | Registrar información |
| PUT | UPDATE | Actualizar registros |
| DELETE | DELETE | Eliminar registros |

## Base de datos

**Nombre:** `db_empresa`

La base de datos utiliza MySQL y contiene las siguientes entidades:

- Cliente
- Empleado
- Puesto

La entidad Empleado está relacionada con Puesto mediante una llave foránea.

## Ejecución del proyecto

1. Clonar el repositorio de GitHub.
2. Abrir la solución en Visual Studio.
3. Restaurar los paquetes NuGet.
4. Configurar la conexión a MySQL mediante secretos de usuario o variables de entorno.
5. Crear o restaurar la base de datos `db_empresa` y sus tablas.
6. Ejecutar la Web API.
7. Acceder a Swagger para probar los endpoints.

**Nota:** Se requiere una base de datos MySQL con el esquema correspondiente. Las credenciales no están incluidas en el repositorio.