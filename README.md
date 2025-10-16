# DBTekus - Sistema de Gestión de Proveedores

Sistema full stack desarrollado con .NET Core y Angular para la gestión de proveedores y servicios.

## Tecnologías

- **Backend**: .NET Core 8, Entity Framework, SQL Server
- **Frontend**: Angular 17, Bootstrap, SweetAlert2
- **Base de Datos**: SQL Server con procedimientos almacenados

## Estructura del Proyecto

### Backend (ApiRestTekus)
- API REST con autenticación JWT
- Controladores para Providers, Services, Countries
- Servicios con inyección de dependencias
- Modelos de datos y DTOs

### Frontend (FrontendTekusAngular)
- Aplicación Angular con componentes modulares
- Gestión de proveedores y servicios
- Modales para CRUD operations
- Tablas dinámicas con búsqueda

### Base de Datos (DBTekus)
- Scripts de creación de base de datos
- Procedimientos almacenados para CRUD
- Diagrama de relación de entidades
- Datos de prueba

## Características

- ✅ Autenticación JWT
- ✅ CRUD completo de proveedores y servicios
- ✅ Gestión de países por servicio
- ✅ Interfaz responsive
- ✅ Validaciones frontend y backend
- ✅ Manejo de errores

## LibraryDBApi

Librería personalizada desarrollada para simplificar las operaciones de base de datos en .NET Core. Proporciona:

- **Ejecución de procedimientos almacenados** con manejo automático de parámetros
- **Mapeo automático** de resultados a modelos de datos
- **Manejo de errores** centralizado y consistente
- **Tiempo de respuesta optimizado** para operaciones CRUD
- **Interfaz amigable** para integración en servicios

Esta librería reduce significativamente el código boilerplate y mejora la mantenibilidad de las operaciones de base de datos.