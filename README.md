# 🧾 APICoreComisiones

API desarrollada en **.NET 8 Web API** para el cálculo de **comisiones de vendedores** según reglas configuradas,  
El proyecto sigue principios de **Clean Architecture** y **SOLID**, separando responsabilidades entre capas para facilitar la mantenibilidad y extensibilidad.

## 📘 Descripción general

El sistema API permite calcular las comisiones de los vendedores con base en:

- Las **ventas registradas** en un rango de fechas (`FechaInicio`, `FechaFin`).
- Las **reglas de comisión** almacenadas en base de datos (`MontoMinimo`, `Porcentaje`).

El cálculo se realiza aplicando la política **Flat**:  
> “El vendedor recibe un porcentaje único sobre el total de sus ventas según el tramo más alto que cumple.”

---

## 🏗️ Arquitectura del proyecto

El proyecto utiliza una **estructura por capas**, inspirada en *Clean Architecture*:
```
APICoreComisiones
│
├── Controllers/ # Endpoints HTTP (API pública)
│ └── CommissionController.cs
│
├── Application/ # Lógica de aplicación y orquestación
│ ├── ICommissionService.cs
│ └── CommissionService.cs
│
├── Domain/ # Lógica de negocio (reglas puras)
│ ├── ICommissionPolicy.cs
│ └── FlatCommissionPolicy.cs
│
├── Data/ # Acceso a base de datos (EF Core)
│ └── AppDbContext.cs
│
├── Models/ # Entidades persistentes
│ ├── Regla.cs
│ ├── Vendedor.cs
│ └── Venta.cs
│
├── ViewModels/ # Contratos de API (entrada/salida)
│ ├── CalculateCommissionVm.cs
│ └── CommissionRowVm.cs
│ 
│
├── appsettings.json # Configuración (cadena de conexión)
└── Program.cs # Configuración principal
```
## ⚙️ Flujo de ejecución

1. El frontend (Angular) envía una solicitud:
   ```json
   {
     "fechaInicio": "2025-10-01T00:00:00Z",
     "fechaFin": "2025-10-31T23:59:59Z"
   }
   ```
2. CommissionController valida las fechas y llamas al servicio
3. CommissionService:
   - FIltra ventas en ese rango
   - Agrupa por vendedor y suma montos
   - Carga las reglas
   - Usa FlatCommissionPolicy para determinar el porcentaje aplicable
   - Calcula comision = total * porcentaje
4. La API responde con un JSON:
    ```json
   {
     "vendedorId": 2,
     "vendedor": "Luis",
     "totalVentas": 26000,
     "porcentajeAplicado": 0.15,
     "comisionCalculada": 3900
   }

## EJECUCIÓN DEL PROYECTO
1. Clonar el repositorio
  - git clone https://github.com/tuusuario/APICoreComisiones.git
  - cd APICoreComisiones
2. Configurar la cadena de conexión en appsettings.json
3. Crear la base de datos y migraciones
4. Ejecutar la API
