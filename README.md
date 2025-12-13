# Users Service: Documentación Completa

Este repositorio contiene el **Users Service**, un microservicio desarrollado en **.NET** encargado de la **gestión y autenticación de usuarios** dentro de una arquitectura basada en microservicios.

El servicio permite crear usuarios, actualizar información básica, realizar eliminación lógica, consultar usuarios y autenticar mediante nombre de usuario o correo electrónico.

---

## Tecnologías Utilizadas

* .NET SDK (Versión 8.0)
* ASP.NET Core Web API
* C#
* Swagger / OpenAPI
* Git
* GitHub Actions (CI/CD)
* Render (Despliegue)

> **Nota:** Este servicio utiliza **almacenamiento en memoria** con fines académicos. No persiste datos en base de datos.

---

## Arquitectura Utilizada

La arquitectura utilizada es **Microservicios**, donde este servicio cumple el rol de **gestión de identidad de usuarios**.

A nivel interno, el microservicio implementa una **arquitectura por capas (Layered Architecture)**:

* Controllers
* Services
* Interfaces
* Models
* DTOs
* Mappers

---

## Patrones de Diseño Utilizados

* **Inyección de Dependencias**  
  Permite desacoplar el controlador del servicio concreto.

* **DTO (Data Transfer Object)**  
  Evita exponer directamente las entidades de dominio.

* **Mapper Pattern**  
  Centraliza la conversión entre entidades y DTOs.

* **Eliminación lógica**  
  Implementada mediante el campo `UserStatus`.

---

## Modelo de Datos

### User

| Campo | Descripción |
|------|------------|
| Id | Identificador único (UUID / Guid) |
| FullName | Nombre completo |
| Email | Correo electrónico |
| Username | Nombre de usuario |
| Password | Contraseña (texto plano, académico) |
| DateOfBirth | Fecha de nacimiento |
| Address | Dirección |
| PhoneNumber | Número de contacto |
| UserStatus | Estado del usuario |

---

## DTOs Implementados

* UserCreateDto  
* UserUpdateDto  
* UserResponseDto  
* LoginUserDto  

---

## Endpoints REST Expuestos

| Método | Endpoint | Descripción |
|------|---------|-------------|
| POST | `/api/users` | Crear usuario |
| GET | `/api/users` | Obtener usuarios |
| GET | `/api/users/{id}` | Obtener usuario por ID |
| PATCH | `/api/users/{id}` | Actualizar usuario |
| DELETE | `/api/users/{id}` | Eliminación lógica |
| POST | `/api/users/login` | Autenticación |

---


## Autenticación

El sistema permite autenticación mediante:

* Nombre de usuario **o**
* Correo electrónico

junto con contraseña.  
Solo usuarios activos pueden autenticarse.

---

## CI/CD (Integración y Despliegue Continuo)

El proyecto cuenta con un **pipeline de CI/CD** implementado mediante **GitHub Actions**, el cual:

1. Se ejecuta automáticamente en cada `push` o `pull request`.
2. Compila el proyecto.
3. Restaura dependencias.
4. Ejecuta validaciones de build.
5. Despliega automáticamente en **Render** si el pipeline es exitoso.

Esto asegura integraciones continuas y despliegues automatizados.

---

## Despliegue en Render

El servicio se encuentra desplegado en **Render**, utilizando un entorno cloud administrado.

Características del despliegue:

* Build automático desde GitHub.
* Variables de entorno configuradas en Render.
* Swagger habilitado en producción.
* Terminación SSL gestionada por Render.

---

## Documentación de la API (Swagger)

Swagger se encuentra habilitado tanto en **desarrollo** como en **producción**.

Acceso:

https://{url-del-servicio}/swagger


---

## Instalación y Ejecución Local

### Clonar Repositorio

```bash
git clone https://github.com/tu-usuario/users-service.git
cd users-service
```

### Restaurar Dependencias

```bash
dotnet restore
```

### Ejecutar Proyecto

```bash
dotnet run
```


## Consideraciones Importantes

- Almacenamiento en memoria.

- Datos no persistentes.

- Contraseñas sin cifrado (solo académico).

- Preparado para integración con otros microservicios.