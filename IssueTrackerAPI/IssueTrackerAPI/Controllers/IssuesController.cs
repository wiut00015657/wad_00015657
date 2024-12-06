using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IssueTrackerAPI.Models;
using AutoMapper;
using IssueTrackerAPI.Repositories;
using IssueTrackerAPI.DTOs;
using Swashbuckle.AspNetCore.Annotations;

namespace IssueTrackerAPI.Controllers
{
    /// <summary>
    /// Controller for managing Issue-related operations in the Issue Tracker API. ID: 00015657
    /// </summary>
    /// <remarks>
    /// Provides endpoints for CRUD operations on Issue entities.
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IRepository<Issue> _issueRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="IssuesController"/> class.
        /// </summary>
        /// <param name="issueRepository">Repository for accessing Issue data.</param>
        /// <param name="mapper">Mapper for transforming entities to DTOs and vice versa.</param>
        public IssuesController(IRepository<Issue> issueRepository, IMapper mapper)
        {
            _issueRepository = issueRepository;
            _mapper = mapper;
        }

        // GET: api/Issues
        /// <summary>
        /// Retrieves all issues from the database.
        /// </summary>
        /// <returns>List of issues as DTOs.</returns>
        /// <response code="200">Returns all issues.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [SwaggerResponse(200, "Returns All Issues", typeof(IEnumerable<IssueDTO>))]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult<IEnumerable<Issue>>> GetIssueDbSet()
        {
            var issues = await _issueRepository.GetAllAsync();
            var issueDtos = _mapper.Map<IEnumerable<IssueDTO>>(issues);
            return Ok(issueDtos);
        }

        // GET: api/Issues/5
        /// <summary>
        /// Retrieves a specific issue by ID.
        /// </summary>
        /// <param name="id">The ID of the issue to retrieve.</param>
        /// <returns>The issue as a DTO.</returns>
        /// <response code="200">Returns the requested issue.</response>
        /// <response code="404">Issue not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Returns Issue", typeof(IssueDTO))]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult<Issue>> GetIssue(int id)
        {
            var issue = await _issueRepository.GetByIdAsync(id);
            if (issue == null)
            {
                return NotFound();
            }

            var issueDto = _mapper.Map<IssueDTO>(issue);
            return Ok(issueDto);
        }

        // PUT: api/Issues/5
        /// <summary>
        /// Updates an existing issue.
        /// </summary>
        /// <param name="id">The ID of the issue to update.</param>
        /// <param name="issueDto">The updated issue data.</param>
        /// <returns>No content.</returns>
        /// <response code="204">Update successful.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("{id}")]
        [SwaggerResponse(204, "Returns No Content")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> PutIssue(int id, IssueDTO issueDto)
        {
            if (id != issueDto.Id)
            {
                return BadRequest();
            }

            var issue = _mapper.Map<Issue>(issueDto);
            await _issueRepository.UpdateAsync(issue);
            return NoContent();
        }

        // POST: api/Issues
        /// <summary>
        /// Creates a new issue.
        /// </summary>
        /// <param name="issueDto">The data for the new issue.</param>
        /// <returns>The created issue as a DTO.</returns>
        /// <response code="201">Issue created successfully.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [SwaggerResponse(201, "Returns Created")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult<Issue>> PostIssue(IssueDTO issueDto)
        {
            var issue = _mapper.Map<Issue>(issueDto);
            await _issueRepository.CreateAsync(issue);
            var createdIssueDto = _mapper.Map<IssueDTO>(issue);
            return Ok();
        }

        // DELETE: api/Issues/5
        /// <summary>
        /// Deletes an issue by ID.
        /// </summary>
        /// <param name="id">The ID of the issue to delete.</param>
        /// <returns>No content.</returns>
        /// <response code="204">Delete successful.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Issue not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Returns No Content")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> DeleteIssue(int id)
        {
            var issue = await _issueRepository.GetByIdAsync(id);
            if (issue == null)
            {
                return NotFound();
            }

            await _issueRepository.DeleteAsync(id);
            return NoContent();
        }

    }
}
