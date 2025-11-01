# WeatherHub

WeatherHub is a desktop and server application that provides weather forecasts.  
The client is built with **WPF**, and the server is an **ASP.NET Core Web API**.  
Shared DTOs and models ensure consistent data exchange between the client and server.


## Features

- Fetch weather forecasts for cities worldwide using **OpenWeatherMap API**.
- Validate city names on both client and server sides.
- Cache weather data for **12 hours (TTL)** to reduce redundant API calls.
- Clean **MVVM architecture** for the WPF client.
- Shared library for DTOs, models, and validation to avoid duplication.
- Two main server controllers:
    1. **CityCoordinateController** – retrieves city coordinates.
    2. **WeatherController** – retrieves weather data by coordinates.
- Only the WPF client is allowed to access the server (CORS/API key restriction).


## Project Structure

```
WeatherHub/
├── WeatherHubShared/                # Folder containing the Shared Class Library
│   └── WeatherHub.Shared/           # Actual Class Library project
│       └── DTOs/                    # Data Transfer Objects (DTOs)
│
├── WeatherHubClient/                # Folder containing the WPF Client project
│   └── WeatherHubClient/            # Actual WPF project
│       ├── Models/
│       ├── Services/
│       ├── Utils/
│       ├── Validation/
│       ├── ViewModels/
│       ├── Views/
│       ├── App.config
│       ├── App.xaml
│       └── AssemblyInfo.cs
│
├── WeatherHubServer/                # Folder containing the Server project
│       ├── Composition/
│       ├── Core/
│       ├── Data/
│       ├── Infrastructure/
│       ├── Services/
│       ├── Utils/
│       └── WeatherHubServer/        # Actual ASP.NET Core Web API project
│           ├── Controllers/
│           │   ├── CityCoordinateController.cs
│           │   └── WeatherController.cs
|           └── appsettings.json

```

- **Shared Library** → contains DTOs, Models, and Validation shared by client and server.
- **Client (WPF)** → structured according to MVVM: Models, ViewModels, Views, Services, Validation, Utils.
- **Server** → separated Controllers, Services, Core, Data, Infrastructure.
- **Controllers** → `CityCoordinateController` for coordinates, `WeatherController` for weather.



## Technologies

- **Client**: WPF, .NET 7, MVVM pattern
- **Server**: ASP.NET Core Web API, Entity Framework Core, SQLite
- **Shared**: .NET Standard Class Library
- **Other**: AutoMapper, MemoryCache, HttpClient, OpenWeatherMap API




## Installation & Setup

To get the application running, follow these steps in your terminal:

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/Hadar-Gerashi/WeatherHub.git
    ```

2.  **Restore packages and build the solution:**
    ```bash
    dotnet restore
    dotnet build
    ```

3.  **Run the Server (ASP.NET Core Web API):**
    ```bash
    cd WeatherHubServer
    dotnet run
    ```
    *Ensure the server is running before starting the client.*

4.  **Run the Client (WPF Application):**
    ```bash
    cd WeatherHubClient
    dotnet run
    ```



## Usage Guide

1.  Open the **WPF client** application.
2.  **Enter a city name** into the search field. The client validates the input using `CityNameValidator`.
3.  The client sends a request to the server, which follows this logic:
    * **Cache Check**: If weather data exists in the server cache (TTL 12 hours) $\rightarrow$ returns cached data immediately.
    * **External API Call**: Otherwise $\rightarrow$ requests data from the **OpenWeatherMap API** $\rightarrow$ saves the result in the server cache $\rightarrow$ returns the result.
4.  The current weather forecast is displayed in the client UI.



## API Endpoints

The server exposes two main controllers for data retrieval and management.

### CityCoordinateController (City Management)

| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `GET` | `/api/citycoordinate` | Returns all tracked cities. |
| `GET` | `/api/citycoordinate/{id}` | Returns a specific city by its ID. |
| `GET` | `/api/citycoordinate/by-name?name={cityName}` | Returns a city by name. |
| `POST` | `/api/citycoordinate` | Adds a new city. |
| `DELETE` | `/api/citycoordinate/{id}` | Deletes a city. |
| `PUT` | `/api/citycoordinate/{id}/last-searched` | Updates the last searched timestamp for a city. |

### WeatherController (Weather Data)

| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `GET` | `/api/weather?city={cityName}` | Returns weather data for the specified city. |



## Caching Strategy

| Feature | Details |
| :--- | :--- |
| **Implementation** | Implemented with `MemoryCache` on the server. |
| **TTL (Time To Live)** | **12 hours**. |
| **Goal** | Reduces the number of API requests to OpenWeatherMap, saving costs and improving response time for repeated queries. |



## Important Notes

* **Access Restriction**: **Only the WPF client is allowed** to access the server's API endpoints (enforced through API key restriction).
* **Configuration**: Ensure your **OpenWeatherMap API key** is correctly set in the `appsettings.json` file of the server project.
* **Architecture**: Client code adheres to **MVVM principles** for maintainability.
* **Code Sharing**: Validation logic is centralized in the `WeatherHub.Shared` library to avoid code duplication across the client and server.
