# Hollywood Stars

Hollywood Stars is a web application built using ASP.NET MVC that allows users to manage actors and their movie associations. You can add, update, and remove actors, and associate them with movies through an intuitive user interface. This application is designed to provide a clean separation of concerns and flexibility in managing actor-movie relationships.

## Features

- Add, update, and delete actors.
- Add, update, and delete movies.
- Manage movie associations for each actor.
- Clean and responsive user interface built with Bootstrap.
- Powered by SQL Server and ASP.NET MVC.

## Getting Started

### Prerequisites

To run this project locally, ensure you have the following installed:

- [Visual Studio](https://visualstudio.microsoft.com/) (2019 or later) with ASP.NET development workload.
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express or full version).
- [Git](https://git-scm.com/) (for cloning the repository).

### Installation

1. **Clone the repository:**

    ```bash
    git clone https://github.com/YOUR_USERNAME/HollywoodStars.git
    cd HollywoodStars
    ```

2. **Open the project in Visual Studio:**

    - Open `HollywoodStars.sln` in Visual Studio.

3. **Set up the database:**

    - Open SQL Server Management Studio (SSMS).
    - Create a new database, e.g., `HollywoodStarsDB`.
    - Open the `HollywoodStarsDB_script.sql` file located in the `Database` folder in SSMS.
    - Execute the script to create the tables and insert the initial data.

4. **Update the connection string:**

    - Open the `Web.config` file.
    - Find the `ConnectionStrings` section and update the `DefaultConnection` with your SQL Server credentials and the `HollywoodStarsDB` database name.

    Example:
    ```xml
    <connectionStrings>
        <add name="DefaultConnection" 
             connectionString="Server=YOUR_SERVER_NAME;Database=HollywoodStarsDB;Trusted_Connection=True;" 
             providerName="System.Data.SqlClient" />
    </connectionStrings>
    ```

5. **Run the application:**

    - In Visual Studio, set the project as the startup project.
    - Press `F5` to run the application.

### Usage

Once the application is running, you can navigate to the following sections:

- **Manage Actors:** View, add, update, and delete actors.
- **Manage Movies:** View, add, update, and delete movies.
- **Manage Actor-Movie Associations:** Link actors to movies or remove associations.

### Database Setup

To set up the database manually, execute the SQL script file:

1. Open SQL Server Management Studio.
2. Connect to your SQL Server instance.
3. Open the `HollywoodStarsDB_script.sql` file.
4. Execute the script to create the necessary database schema and insert the initial data.

### Technology Stack

- **Frontend:** HTML, CSS, Bootstrap, Razor.
- **Backend:** ASP.NET MVC (C#).
- **Database:** SQL Server.

## Contributing

Feel free to fork this repository, make changes, and submit pull requests. For major changes, please open an issue to discuss the proposed changes first.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
