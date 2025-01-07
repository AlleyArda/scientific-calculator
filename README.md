
# Scientific Calculator Web Application (Only back-end codes)

A comprehensive scientific calculator application with a full-stack implementation using **React** (front-end) and **ASP.NET Core** (back-end). This project demonstrates modern development practices, including **layered architecture**, adherence to **SOLID principles**, **CORS management**, and **session handling**.

---

## Features

### Back-End Features
- **User Authentication:** Login and register functionality with secure session management.
- **Calculation History:** Store, retrieve, and delete user-specific calculations.
- **Layered Architecture:** Decoupled structure for scalability and maintainability.
- **Database Integration:** Persistent data storage with PostgreSQL.

### Front-End Features
- **Dynamic User Interface:** Responsive and interactive UI for scientific calculations.
- **Calculation History Management:** View and delete previous calculations.
- **Session Integration:** Cookie-based authentication for secure API access.

---

## Tech Stack

### Back-End
- **Language:** C# (ASP.NET Core)
- **Database:** PostgreSQL
- **Architecture:** Layered architecture with SOLID principles
- **Session Management:** Distributed memory cache and cookie-based sessions
- **API Testing:** Swagger for API documentation and testing

### Front-End
- **Language:** JavaScript
- **Framework:** React with React-Bootstrap
- **State Management:** useState and useEffect hooks

---

## Key Design Details

### Layered Architecture
This project is built using a layered architecture, which separates concerns and improves maintainability. The key layers include:
- **API Layer:** Handles HTTP requests and responses.
- **Service Layer:** Contains the business logic.
- **Data Layer:** Manages interactions with the PostgreSQL database.
- **DataAccess Layer:** Implements repositories and data access logic.

### SOLID Principles
The project follows the **SOLID principles** to ensure code quality:
- **S**ingle Responsibility Principle: Each class is responsible for a single functionality.
- **O**pen/Closed Principle: The system is open for extension but closed for modification.
- **L**iskov Substitution Principle: Derived classes can replace base classes without altering the program's correctness.
- **I**nterface Segregation Principle: Clients depend only on the interfaces they use.
- **D**ependency Inversion Principle: High-level modules do not depend on low-level modules, but both depend on abstractions.

### NuGet Packages
The following NuGet packages are used in the project:
- `Microsoft.EntityFrameworkCore` and `Microsoft.EntityFrameworkCore.Tools`: For database interaction.
- `Npgsql.EntityFrameworkCore.PostgreSQL`: For PostgreSQL support in Entity Framework Core.
- `Swashbuckle.AspNetCore`: To generate Swagger API documentation.
- `Microsoft.AspNetCore.Session`: For session management.
- `Microsoft.Extensions.Caching.Memory`: For distributed memory cache.

---

## Why This Project?

This project showcases my ability to:
1. Design and implement full-stack applications with modern technologies.
2. Apply software engineering principles like SOLID and maintainable architectural patterns.
3. Build secure, scalable, and user-friendly applications.
4. Work collaboratively across front-end and back-end technologies to deliver an integrated solution.

---

## Contact

Developed by **Ali Arda Kulaksız**  
📧 Email: aliardakulaksiz@gmail.com  
🌐 GitHub: [github.com/AlleyArda](https://github.com/AlleyArda)  
📍 Open to opportunities in software development.

---

## Setup & Run

### Prerequisites
1. Install **.NET SDK 6.0+** for back-end development.
2. Install **Node.js** and **npm** for front-end development.
3. Set up **PostgreSQL** on your local or remote server.

### Back-End Setup
1. Clone the repository:
   ```bash
   git clone https://github.com/AlleyArda/scientific-calculator.git
   ```
2. Navigate to the back-end directory:
   ```bash
   cd backend
   ```
3. Configure the connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=calculator_db;Username=your_username;Password=your_password"
   }
   ```
   Replace `your_username` and `your_password` with your PostgreSQL credentials.
4. Restore the necessary NuGet packages:
   ```bash
   dotnet restore
   ```
5. Run the back-end application:
   ```bash
   dotnet run
   ```
