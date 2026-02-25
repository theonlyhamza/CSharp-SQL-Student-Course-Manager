using System;
using System.Data;
using Microsoft.Data.SqlClient;

class Program
{
    private const string ConnStr =
    "Server=HAMZA;Database=StudentCourseDB;Trusted_Connection=True;TrustServerCertificate=True;";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n=== Student Course Manager ===");
            Console.WriteLine("1) Add Student");
            Console.WriteLine("2) Add Course");
            Console.WriteLine("3) Enroll Student in Course");
            Console.WriteLine("4) View Report (Students + Courses)");
            Console.WriteLine("0) Exit");
            Console.Write("Choose: ");

            var choice = Console.ReadLine();
            try
            {
                switch (choice)
                {
                    case "1": AddStudent(); break;
                    case "2": AddCourse(); break;
                    case "3": Enroll(); break;
                    case "4": Report(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static void AddStudent()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Department: ");
        string dept = Console.ReadLine() ?? "";

        Console.Write("GPA (e.g., 3.45): ");
        if (!decimal.TryParse(Console.ReadLine(), out var gpa))
        {
            Console.WriteLine("Invalid GPA.");
            return;
        }

        const string sql = @"
INSERT INTO Students (Name, Department, GPA)
VALUES (@Name, @Department, @GPA);
SELECT SCOPE_IDENTITY();";

        using var con = new SqlConnection(ConnStr);
        using var cmd = new SqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@Name", name);
        cmd.Parameters.AddWithValue("@Department", dept);
        cmd.Parameters.AddWithValue("@GPA", gpa);

        con.Open();
        var newId = Convert.ToInt32(cmd.ExecuteScalar());
        Console.WriteLine($"Student added. StudentID = {newId}");
    }

    static void AddCourse()
    {
        Console.Write("Course Name: ");
        string course = Console.ReadLine() ?? "";

        const string sql = @"
INSERT INTO Courses (CourseName)
VALUES (@CourseName);
SELECT SCOPE_IDENTITY();";

        using var con = new SqlConnection(ConnStr);
        using var cmd = new SqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@CourseName", course);

        con.Open();
        var newId = Convert.ToInt32(cmd.ExecuteScalar());
        Console.WriteLine($"Course added. CourseID = {newId}");
    }

    static void Enroll()
    {
        Console.Write("StudentID: ");
        if (!int.TryParse(Console.ReadLine(), out var studentId))
        {
            Console.WriteLine("Invalid StudentID.");
            return;
        }

        Console.Write("CourseID: ");
        if (!int.TryParse(Console.ReadLine(), out var courseId))
        {
            Console.WriteLine("Invalid CourseID.");
            return;
        }

        const string sql = @"
INSERT INTO Enrollments (StudentID, CourseID)
VALUES (@StudentID, @CourseID);";

        using var con = new SqlConnection(ConnStr);
        using var cmd = new SqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@StudentID", studentId);
        cmd.Parameters.AddWithValue("@CourseID", courseId);

        con.Open();
        cmd.ExecuteNonQuery();
        Console.WriteLine("Enrollment added.");
    }

    static void Report()
    {
        const string sql = @"
SELECT 
    s.StudentID,
    s.Name,
    s.Department,
    s.GPA,
    c.CourseName
FROM Students s
LEFT JOIN Enrollments e ON s.StudentID = e.StudentID
LEFT JOIN Courses c ON e.CourseID = c.CourseID
ORDER BY s.StudentID, c.CourseName;";

        using var con = new SqlConnection(ConnStr);
        using var cmd = new SqlCommand(sql, con);
        con.Open();

        using var rdr = cmd.ExecuteReader();
        Console.WriteLine("\n--- Report: Students + Courses ---");
        Console.WriteLine("ID | Name | Dept | GPA | Course");

        while (rdr.Read())
        {
            int id = rdr.GetInt32(0);
            string name = rdr.GetString(1);
            string dept = rdr.GetString(2);
            decimal gpa = rdr.GetDecimal(3);
            string course = rdr.IsDBNull(4) ? "(No Course)" : rdr.GetString(4);

            Console.WriteLine($"{id} | {name} | {dept} | {gpa} | {course}");
        }
    }
}