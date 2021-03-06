﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Text;

namespace KevinSharp.DataModel
{
    public class KevinSharpDbContext : DbContext
    {
		public DbSet<Course> Courses { get; set; }

        public DbSet<TimeSlotGroup> TimeSlotGroups { get; set; }

        public DbSet<TimeSlot> TimeSlots { get; set; }

        public DbSet<Applicant> Applicants { get; set; }

        public DbSet<SessionLog> SessionLogs { get; set; }

        public KevinSharpDbContext() : this("Name=KevinSharpDBConnection") { }

        public KevinSharpDbContext(string connectionStringId) : base(connectionStringId) 
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
        }
    }
}