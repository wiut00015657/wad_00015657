using IssueTrackerAPI.Data;
using IssueTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IssueTrackerAPI.Repositories
{
    /// <summary>
    /// Repository implementation for managing Employee entities.
    /// </summary>
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly GeneralDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeRepository"/> class.
        /// </summary>
        /// <param name="context">The database context for Employee operations.</param>
        public EmployeeRepository(GeneralDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new Employee entity in the database.
        /// </summary>
        /// <param name="entity">The Employee entity to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task CreateAsync(Employee entity)
        {
            await _context.EmployeeDbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an Employee entity from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the Employee entity to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteAsync(int id)
        {
            var employee = await _context.EmployeeDbSet.FindAsync(id);
            if (employee != null)
            {
                _context.EmployeeDbSet.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retrieves all Employee entities from the database.
        /// </summary>
        /// <returns>A task that returns a collection of all Employee entities.</returns>
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.EmployeeDbSet.ToListAsync();
        }

        /// <summary>
        /// Retrieves an Employee entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the Employee entity to retrieve.</param>
        /// <returns>A task that returns the Employee entity if found, or null if not.</returns>
        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.EmployeeDbSet.FindAsync(id);
        }

        /// <summary>
        /// Updates an existing Employee entity in the database.
        /// </summary>
        /// <param name="entity">The Employee entity with updated values.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(Employee entity)
        {
            _context.EmployeeDbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
