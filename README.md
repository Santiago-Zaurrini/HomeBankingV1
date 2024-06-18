# Homebanking - Banco Vinotinto 🏦

HomeBanking es una aplicación web desarrollada para el Banco Vinotinto, que permite a los usuarios gestionar sus cuentas bancarias, tarjetas de crédito y débito, realizar transferencias y solicitar préstamos. La aplicación está construida como una Web API utilizando ASP.NET Core 8 y emplea varios patrones de diseño como Generics, Repository, Service Layer, DTO, y Builder, junto con la inyección de dependencias para asegurar una arquitectura sólida y mantenible.

## Características Principales ⚡

- Autenticación: El sistema utiliza autenticación basada en cookies para gestionar las sesiones de usuario. Aunque también se ha implementado la autenticación por JWT, esta no está integrada en el frontend.
- Registro de Usuarios: Los usuarios pueden registrarse en la plataforma proporcionando la información requerida.
- Gestión de Cuentas: Los usuarios pueden crear y gestionar sus cuentas bancarias.
- Tarjetas de Crédito y Débito: Los usuarios pueden solicitar y gestionar tarjetas de crédito y débito.
- Transferencias: Permite a los usuarios realizar transferencias a diferentes cuentas bancarias.
- Préstamos: Los usuarios pueden solicitar préstamos y gestionar sus pagos.


## Tecnologías Utilizadas 💻

- **Framework**: ASP.NET Core 8
- **Autenticación**: Cookies (JWT no implementado en frontend)
- **Base de Datos**: Microsoft SQL Server
- **Patrones de Diseño**: Generics, Repository, Service Layer, DTO, Builder, Inyección de Dependencias


## Instalación & Configuración

**Prerrequisitos**
- .NET Core SDK 8
- SQL Server


Configuración del proyecto

1 - Clonar el repositorio.

2 - Configurar la cadena de conexión a tu base de datos.
```bash
{
  "ConnectionStrings": {
    "MyDbConnection": "Server=TU_SERVIDOR;Database=DbNet8v1;Trusted_Connection=True;TrustServerCertificate=true"
  },
}
```
3 - Ejecutar migraciones y actualizar base de datos.    
```bash
Add-Migration nombreMigración
Update-Database
```
4 - Ejecutar la aplicación.


## Patrones de Diseño

- **Generics**: Se utilizan generics para crear repositorios que puedan manejar diferentes tipos de entidades de manera genérica y reutilizable.

- **Repository**: El patrón Repository se utiliza para abstraer las operaciones de acceso a datos, proporcionando una capa intermedia entre la lógica de negocio y la capa de datos.

- **Service Layer**: La capa de servicios actúa como intermediaria entre la capa de presentación (controladores) y la capa de datos (repositorios), conteniendo la lógica de negocio de la aplicación.

- **DTO (Data Transfer Object)**: Los DTO se utilizan para transferir datos entre diferentes capas de la aplicación, especialmente entre la capa de servicios y la capa de presentación, sin exponer las entidades del dominio directamente.

- **Builder**: El patrón Builder se emplea para construir objetos complejos de manera incremental, proporcionando una forma flexible y controlada de crear instancias de clases.

- **Inyección de Dependencias**: La inyección de dependencias se utiliza para gestionar las dependencias entre los diferentes componentes de la aplicación, facilitando la mantenibilidad y las pruebas unitarias.

## Autor 📝

- [@Santiago-Zaurrini](https://github.com/Santiago-Zaurrini)
- Email: santijz07@gmail.com
