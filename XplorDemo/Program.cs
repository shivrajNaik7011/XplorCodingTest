using System;
using System.Linq;

namespace XplorDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new EnrollmentContext())
            {
                bool addMoreEnrollment = true;

                while (addMoreEnrollment)
                {
                    try
                    {
                        //User data input 
                        Console.WriteLine("Enter user details:");
                        Console.Write("First Name: ");
                        string firstName = Console.ReadLine();
                        Console.Write("Last Name: ");
                        string lastName = Console.ReadLine();
                        Console.Write("Date of Birth (yyyy-MM-dd): ");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth))
                        {
                            Console.WriteLine("Invalid date format.");
                            continue;
                        }

                        // Check if the user already exists
                        var existingUser = context.Users.FirstOrDefault(u => u.FirstName == firstName && u.LastName == lastName && u.DateOfBirth == dateOfBirth);
                        if (existingUser != null)
                        {
                            Console.WriteLine("User already exists. Enrollment not added.");
                            continue; // Skip adding the enrollment
                        }
                        //Course data input 

                        Console.WriteLine("Enter course details:");
                        Console.Write("Course Name: ");
                        string courseName = Console.ReadLine();

                        // Check if the course already exists
                        var existingCourse = context.Courses.FirstOrDefault(c => c.Name == courseName);
                        Course course;
                        if (existingCourse != null)
                        {
                            course = existingCourse; // Use existing course reference
                        }
                        else
                        {
                            // Add the new course if it doesn't exist
                            Console.Write("Course Description (optional): ");
                            string courseDescription = Console.ReadLine();
                            course = new Course
                            {
                                Name = courseName,
                                Description = courseDescription
                            };
                            context.Courses.Add(course);
                        }

                        // Create and add a new user
                        var user = new User
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            DateOfBirth = dateOfBirth
                        };
                        context.Users.Add(user);

                        // Add enrollment
                        Console.WriteLine("Enter enrollment details:");
                        Console.Write("Start Date (yyyy-MM-dd): ");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                        {
                            Console.WriteLine("Invalid date format.");
                            continue;
                        }
                        Console.Write("End Date (optional, leave blank if none): ");
                        DateTime? endDate = null;
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime tempEndDate))
                        {
                            endDate = tempEndDate;
                        }
                        var enrollment = new Enrolment
                        {
                            StartDate = startDate,
                            EndDate = endDate,
                            Course = course,
                            User = user
                        };
                        context.Enrolments.Add(enrollment);

                        // Save changes to the database
                        context.SaveChanges();

                        Console.WriteLine("Details added successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }

                    // Ask the user if they want to add one more enrollment
                    Console.Write("Do you want to add one more enrollment? (yes/no): ");
                    string input = Console.ReadLine().Trim().ToLower();
                    addMoreEnrollment = (input == "yes");
                }
            }
        }
    }
}
