using IssueTrackerAPI.Data;
using IssueTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IssueTrackerAPI.Repositories
{
    /// <summary>
    /// Repository implementation for managing Issue entities.
    /// </summary>
    public class IssueRepository : IRepository<Issue>
    {
        private readonly GeneralDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="IssueRepository"/> class.
        /// </summary>
        /// <param name="context">The database context for Issue operations.</param>
        public IssueRepository(GeneralDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new Issue entity in the database.
        /// </summary>
        /// <param name="entity">The Issue entity to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task CreateAsync(Issue entity)
        {
            await _context.IssueDbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an Issue entity from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the Issue entity to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteAsync(int id)
        {
            var issue = await _context.IssueDbSet.FindAsync(id);
            if (issue != null)
            {
                _context.IssueDbSet.Remove(issue);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retrieves all Issue entities from the database, including related Employee data.
        /// </summary>
        /// <returns>A task that returns a collection of all Issue entities with related Employee data.</returns>
        public async Task<IEnumerable<Issue>> GetAllAsync()
        {
            return await _context.IssueDbSet.Include(e => e.Employee).ToListAsync();
        }

        /// <summary>
        /// Retrieves an Issue entity by its ID, including related Employee data.
        /// </summary>
        /// <param name="id">The ID of the Issue entity to retrieve.</param>
        /// <returns>A task that returns the Issue entity if found, or null if not.</returns>
        public async Task<Issue> GetByIdAsync(int id)
        {
            return await _context.IssueDbSet.Include(e => e.Employee).FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Updates an existing Issue entity in the database.
        /// </summary>
        /// <param name="entity">The Issue entity with updated values.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(Issue entity)
        {
            _context.IssueDbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
