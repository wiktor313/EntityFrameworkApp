
# EntityFramework Project

This repository contains a project demonstrating the use of Entity Framework in a WinForms application. The application allows users to interact with a database, perform CRUD operations, and track changes to data entities.

## Features

- Display and edit data from the `Znajomi` and `Mieszkania_domy` tables.
- Track and display changes made to entities before saving them to the database.
- Support for detached entities to ensure data consistency during editing.
- Integration with a stored procedure (`ProceduraMieszkanie`) for specific data retrieval.

## Project Structure

The solution contains two primary forms:

### 1. `Form1`
- Main interface for interacting with the `Znajomi` and `Mieszkania_domy` tables.
- **Key functionalities:**
  - Display data from the `Znajomi` table in a `DataGridView`.
  - Automatically update the `Mieszkania_domy` table based on selected `Pesel` from `Znajomi`.
  - Track changes made to both tables and display them before saving.
  - Save changes to the database using `Entity Framework`.

### 2. `Form2`
- Provides additional functionality for working with the `Znajomi` table.
- **Key functionalities:**
  - Display data using a stored procedure (`ProceduraMieszkanie`) based on a selected `Pesel`.
  - Count and display the number of unsaved changes in the `Znajomi` and `Mieszkania_domy` tables.

## Technologies Used

- **.NET Framework**: WinForms for the graphical user interface.
- **Entity Framework**: For database interaction and change tracking.
- **SQL Server**: Backend database management.

## How to Run

1. Clone this repository:
   ```bash
   git clone https://github.com/your-username/your-repo.git
   cd your-repo
   ```
2. Open the solution in Visual Studio.
3. Ensure that the database connection string in `App.config` points to a valid SQL Server instance with the required database.
4. Build and run the project.

## Future Enhancements

- Add validation to input fields to prevent invalid data entries.
- Implement more robust error handling and logging.
- Extend functionality to include additional tables or features.

## Contributing

Contributions are welcome! Please fork this repository and submit a pull request for any proposed changes.

## License

This project is licensed under the MIT License.
