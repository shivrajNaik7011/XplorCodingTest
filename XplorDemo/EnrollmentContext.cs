using Microsoft.EntityFrameworkCore;
using System;

public class EnrollmentContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Enrolment> Enrolments { get; set; }
    public DbSet<Course> Courses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        string dbFileName = "enrollment.db";
        string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
        int index = projectDirectory.IndexOf("XplorDemo");
        // Extract the first part of the path before "XplorDemo"
        string relativePath = index >= 0 ? projectDirectory.Substring(0, index + "XplorDemo".Length) : projectDirectory;
        string dbFileNameandpath = relativePath+"\\enrollment.db";
        string connectionString = $"Data Source={dbFileNameandpath}";
        //string connectionString = "Data Source=D:\\NewT\\XplorDemo\\enrollment.db";

        optionsBuilder.UseSqlite(connectionString);
    }
}
