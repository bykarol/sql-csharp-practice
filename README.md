# Overview
1. install dependencies
Create the new project
dotnet new console --framework net8.0 --use-program-main

adding dapper and sqlite
dotnet add package Dapper
dotnet add package Microsoft.Data.Sqlite

2. Set up the SQLite database:
Create an SQLite database and tables to store your application data.

3. Create a Database Helper:
Write a helper class to manage the database connections and queries using Dapper.

4. Update Managers to Use Dapper:
Modify the PatientsManager and AppointmentsManager to interact with the database using Dapper.


{Important! Do not say in this section that this is college assignment. Talk about what you are trying to accomplish as a software engineer to further your learning.}

{Provide a description of the software that you wrote and how it integrates with a SQL Relational Database. Describe how to use your program.}

{Describe your purpose for writing this software.}

{Provide a link to your YouTube demonstration. It should be a 4-5 minute demo of the software running, a walkthrough of the code, and a view of how created the Relational Database.}

[Software Demo Video](http://youtube.link.goes.here)

# Relational Database

> While SQL began as a language used to manipulate data in relational databases, it has evolved to be a language for manipulating data across various database technologies.
SQL is not an acronym for anything (although many people will insist it stands for “Structured Query Language”). When referring to the language, it is equally acceptable to say the letters individually (i.e., S. Q. L.) or to use the word sequel.
Alan Beaulieu (2020, O'Reilly Media, Inc.)

## AMS - Appointment Management System - ERD
![ERD](sqlERD.jpeg)         

# Development Environment

- .NET8 SDK
- Visual Studio Code
- C#
- SQLite
- Dapper

## What is Dapper?
> Dapper is a 'micro ORM', providing lightweight, high-performance data access with minimal abstraction. It relies on SQL queries, for example, mapping the results to objects directly:
`SELECT * FROM CUSTOMERS WHERE ID = 1`

# Useful Websites

- [Dapper GitHub Repo](https://github.com/DapperLib/Dapper)
- [Dapper Intro - tutorial in spanish](https://www.youtube.com/watch?v=3eV5m56Rf3E)
- [How to use C# and .NET with SQLite](https://learn.microsoft.com/en-us/shows/on-net/how-do-i-use-csharp-and-dotnet-with-sqlite)


# Future Work

- Using another DataBase motor
- Item 2
- Item 3