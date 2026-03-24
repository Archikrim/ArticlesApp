# Articles & Products Web Application

## 📌 Overview

This is a test web application built with ASP.NET Core that allows users to manage articles and browse products.

The application consists of:

* ASP.NET Web API (backend)
* ASP.NET MVC (frontend)

---

## 🛠️ Tech Stack

**Backend:**

* ASP.NET Core Web API
* Entity Framework Core
* MS SQL Server (or InMemory for testing)

**Frontend:**

* ASP.NET MVC (Razor)
* Tailwind CSS

**Testing:**

* xUnit
* EF Core InMemory

---

## ✨ Features

### Articles

* Full CRUD operations
* Search by title
* Filter by tag
* Pagination
* Article details page

### Products

* Data fetched from external API: https://fakestoreapi.com
* Stored in local database
* Products list page
* Product details page

---

## 🚀 Additional Improvements

* Clean UI using Tailwind CSS
* Server-side pagination with validation
* Unit tests for service layer
* Separation of concerns (Controller → Service → DbContext)

---

## 🧪 Testing

Unit tests are implemented for the service layer using:

* xUnit
* EF Core InMemory provider

Tested scenarios:

* Filtering by tag
* Searching by title
* Pagination logic
* Edge cases (empty results, page overflow)

---

## ▶️ How to Run

1. Clone the repository

```bash
git clone https://github.com/Archikrim/ArticlesApp.git
```

2. Open solution in Visual Studio

3. Set multiple startup projects:

   * API project
   * MVC project

4. Run the application

* API: [https://localhost:xxxx/swagger](https://localhost:xxxx/swagger)
* MVC: [https://localhost:xxxx](https://localhost:xxxx)

---

## 🧠 Architecture

The application follows a layered architecture:

Controller → Service → DbContext

* Controllers handle HTTP requests
* Services contain business logic
* Entity Framework handles data access

---

## 📎 Notes

* InMemory database can be used for testing/demo purposes
---

## 🔮 Future Improvements

* Add Docker support for easier deployment
* Implement caching (e.g., Redis) for better performance
* Introduce repository pattern if the project grows in complexity
---

## 👨‍💻 Author

Artur Suleimanov
Developed as a test task.

<img width="1397" height="1018" alt="image" src="https://github.com/user-attachments/assets/4d2aaec7-2e7f-43d6-b825-fe018c3e3e83" />
<img width="1536" height="813" alt="image" src="https://github.com/user-attachments/assets/cdae6c3c-6580-41df-8678-f7b099b348a6" />
<img width="1912" height="812" alt="image" src="https://github.com/user-attachments/assets/a516dbc2-61f4-43e9-8c0c-b5981ffa005c" />


