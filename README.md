# Address Manager Application

## Overview

This is a simple address manager application developed using Wisej.NET and C#. It allows users to manage organizations and staff members, including their basic contact information. Data is stored using an SQLite database.

## Features

*   **Organizations Management:**
    *   Add, update, and delete organizations.
    *   Each organization includes fields for name, street, zip, city, and country.
    *   Displays organization data in a grid view.
*   **Staff Management:**
    *   Add, update, and delete staff members.
    *   Each staff member includes fields for title, first name, last name, phone number, email, and their associated organization.
    *   Displays staff data in a grid view.
    *   Staff members are associated with organizations via a dropdown selection.
*   **Simple User Interface:** Uses Wisej.NET's basic components for displaying and managing data.
*   **Data Storage:** Uses a local SQLite database file.

## How to Run

1.  **Clone the repository:**
2.  **Open the solution in Visual Studio:** Open the `AddressManager.sln` file.
3.  **Ensure NuGet Packages are Restored:** Visual Studio should restore the NuGet packages defined in `packages.config` automatically. If not, right-click on the project and select "Restore NuGet Packages".
4.  **Compile and Run:** Build (Ctrl+Shift+B) and run (F5) the application.

## Technologies Used

*   Wisej.NET (for UI)
*   C# (.NET Framework 4.8)
*   SQLite (for data storage)

## Time Spent

This project was completed in approximately **4-6 hours**. This time includes planning, coding, testing, and preparing the repository for submission.

## Notes

*   This is a simplified version of the application due to time constraints. It may not have full error handling, robust UI, or all features necessary in a production-level application.
*   The project is intended to demonstrate basic data management and UI creation capabilities using Wisej.NET.

## Author

Ju Zi Hein