# Hospital Management System

A web-based application developed using C# and ASP.NET Core to manage various administrative and operational aspects of a hospital. It streamlines workflows related to patient records, appointments, and staff information.

## 🚑 Features

- Patient registration and management
- Appointment scheduling
- Staff (Doctors, Nurses, Admin) management
- Secure database integration using Entity Framework Core
- Responsive front-end with Bootstrap

## 🧰 Tech Stack

- ASP.NET Core MVC (C#)
- Entity Framework Core
- MS SQL Server
- Bootstrap
- Visual Studio

## 🔧 Setup Instructions

1. Clone the repository:
git clone https://github.com/your-username/hospital_management_system.git

2. Open the solution in **Visual Studio**

3. Configure DB connection in `appsettings.json`

4. Apply migrations:
## Update-Database

5. Run the project!

## 📁 Folder Structure

hospital_management_system/
├── Controllers/
├── Models/
├── Views/
├── wwwroot/
├── appsettings.json
└── ...

## 🔒 Notes

> Sensitive info like DB connection strings should be stored in `appsettings.Development.json` or environment variables, and excluded from source control.

## 📄 License

This project is licensed under the MIT License.
