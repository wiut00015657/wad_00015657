using IssueTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IssueTrackerAPI.Data
{
    /// <summary>
    /// Represents the database context for the Issue Tracker application.
    /// </summary>
    public class GeneralDbContext : DbContext
    {
        
        /// Initializes a new instance of the GeneralDbContext class
        public GeneralDbContext(DbContextOptions<GeneralDbContext> o) : base(o) { }

        
        /// Gets or sets the DbSet for managing Issue entities.
        public DbSet<Issue> IssueDbSet { get; set; }

        
        /// Gets or sets the DbSet for managing Employee entities.
        public DbSet<Employee> EmployeeDbSet { get; set; }
    }
}
