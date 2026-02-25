# CSharp-SQL-Student-Course-Manager
A console-based Student Course Manager built using C# and SQL Server. Implements CRUD operations, ADO.NET connectivity, and relational data handling using JOIN queries.
# 🎓 Student Course Manager (C# + SQL Server)

A console-based Student Course Manager built using **C#** and **SQL Server (SSMS)**.  
This project demonstrates database connectivity using **ADO.NET**, CRUD operations, and relational data handling with JOIN queries.

---

## 🚀 Features

- ➕ Add Student  
- ➕ Add Course  
- 🔗 Enroll Student in Course  
- 📊 View Students with their Enrolled Courses (JOIN Report)  
- 🗂 Uses relational database design (Students, Courses, Enrollments)

---

## 🛠 Technologies Used

- C#
- .NET 8
- SQL Server (SSMS)
- ADO.NET
- SQL (CRUD, INNER JOIN, LEFT JOIN)

---

## 🗄 Database Structure

### Students
- StudentID (Primary Key)
- Name
- Department
- GPA

### Courses
- CourseID (Primary Key)
- CourseName

### Enrollments
- EnrollmentID (Primary Key)
- StudentID (Foreign Key)
- CourseID (Foreign Key)

---

## 📌 What I Learned

- Connecting C# with SQL Server using ADO.NET  
- Writing parameterized queries  
- Implementing CRUD operations  
- Understanding INNER JOIN and LEFT JOIN  
- Basic database normalization  
- OOP fundamentals in C#

---

## ▶️ How to Run

1. Create the database and tables in SQL Server.
2. Update the connection string in `Program.cs` with your SQL Server name.
3. Build and run the console application.
4. Use the menu to manage students and courses.

---

## 📈 Future Improvements

- Add input validation
- Implement update & delete functionality
- Convert to GUI (WinForms or WPF)
- Add authentication system

---

## 👨‍🎓 Author

Hamza  
2nd Semester Data Science Student  
UET Lahore
