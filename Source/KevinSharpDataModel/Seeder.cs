using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace KevinSharp.DataModel
{
    public static class Seeder
    {
        public static void Seed(KevinSharpDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            Course courseCS02 = new Course
            {
                Code = "CS02",
                Name = "C# 5.0 Fundamentals",
                ShortName = "C# Fundamentals",
                Length = "2x 3.5 hours",
                Level = CourseLevel.Beginner,
                Description = "C# 5.0 Fundamentals Course",
                Modules = new string[0],
                TimeSlotGroups = new List<TimeSlotGroup>()
            };

            context.Courses.AddOrUpdate(
                c => c.Code,
                new Course
                {
                    Code = "CS01",
                    Name = "Getting started with .NET and Visual Studio 2013 (coming in June)",
                    ShortName = "Visual Studio 2013",
                    Length = "2x 3.5 hours",
                    Level = CourseLevel.Beginner,
                    Description = "You are going to learn the ins and outs of Visual Studio 2013",
                    Modules = new string[0],
                    TimeSlotGroups = new List<TimeSlotGroup>()
                },

                courseCS02,

                new Course
                {
                    Code = "CS03",
                    Name = "Introduction to ASP.NET 5.0 (coming in July)",
                    ShortName = "ASP.NET 5.0",
                    Length = "2x 3.5 hours",
                    Level = CourseLevel.Beginner,
                    Description = "You are going to learn the ins and outs of ASP.NET",
                    Modules = new string[0],
                    TimeSlotGroups = new List<TimeSlotGroup>()
                },
                new Course
                {
                    Code = "CS04",
                    Name = "ASP.NET MVC 6 Fundamentals (coming in August)",
                    ShortName = "ASP.NET MVC 6",
                    Length = "2x 3.5 hours",
                    Level = CourseLevel.Beginner,
                    Description = "You are going to learn the ins and outs of ASP.NET MVC",
                    Modules = new string[0],
                    TimeSlotGroups = new List<TimeSlotGroup>()
                },
                new Course
                {
                    Code = "CS05",
                    Name = "C# Exotics: Generics, LINQ and Reflection (coming in July)",
                    ShortName = "C# Exotics",
                    Length = "2x 3.5 hours",
                    Level = CourseLevel.Intermediate,
                    Description = "You are going to learn the ins and outs of Generics, LINQ and Reflection with C#",
                    Modules = new string[0],
                    TimeSlotGroups = new List<TimeSlotGroup>()
                },
                new Course
                {
                    Code = "CS06",
                    Name = "Windows Communication Foundation (WCF) Deep Dive (coming in August)",
                    ShortName = "WCF",
                    Length = "2x 3.5 hours",
                    Level = CourseLevel.Intermediate,
                    Description = "You are going to learn the ins and outs of WCF",
                    Modules = new string[0],
                    TimeSlotGroups = new List<TimeSlotGroup>()
                },
                new Course
                {
                    Code = "CS07",
                    Name = "MS-SQL databases and Entity Framework 6 (coming in September)",
                    ShortName = "Entity Framework",
                    Length = "2x 3.5 hours",
                    Level = CourseLevel.Intermediate,
                    Description = "You are going to learn the ins and outs of Entity Framework",
                    Modules = new string[0],
                    TimeSlotGroups = new List<TimeSlotGroup>()
                },
                new Course
                {
                    Code = "CS08",
                    Name = "Building Websites with HTML5, jQuery and Bootstrap (coming in November)",
                    ShortName = "HTML5, jQuery, Bootstrap",
                    Length = "2x 3.5 hours",
                    Level = CourseLevel.Intermediate,
                    Description = "You are going to learn the ins and outs of HTML, jQuery and Bootstrap",
                    Modules = new string[0],
                    TimeSlotGroups = new List<TimeSlotGroup>()
                }
            );


            for (int i = 0; i < 30; i++)
            {
                TimeSlotGroup tsgCS02 = new TimeSlotGroup() { Course = courseCS02, TimeSlots = new List<TimeSlot>() };
                tsgCS02.TimeSlots.Add(new TimeSlot() { StartTimeUtc = new DateTime(2015, 05, 12, 14, 00, 00, DateTimeKind.Utc).AddDays(i * 7), Duration = 210 });
                tsgCS02.TimeSlots.Add(new TimeSlot() { StartTimeUtc = new DateTime(2015, 05, 15, 14, 00, 00, DateTimeKind.Utc).AddDays(i * 7), Duration = 210 });
                tsgCS02.GenerateNewCode();
                context.TimeSlotGroups.AddOrUpdate(tsg => tsg.Code, tsgCS02);

                tsgCS02 = new TimeSlotGroup() { Course = courseCS02, TimeSlots = new List<TimeSlot>() };
                tsgCS02.TimeSlots.Add(new TimeSlot() { StartTimeUtc = new DateTime(2015, 05, 13, 10, 00, 00, DateTimeKind.Utc).AddDays(i * 7), Duration = 420 });
                tsgCS02.GenerateNewCode();
                context.TimeSlotGroups.AddOrUpdate(tsg => tsg.Code, tsgCS02);
            }

            context.SaveChanges();
        }
    }
}
