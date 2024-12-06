using Microsoft.AspNetCore.Mvc;
using IssueTrackerAPI.Models;
using IssueTrackerAPI.Repositories;
using AutoMapper;
using IssueTrackerAPI.DTOs;
using Swashbuckle.AspNetCore.Annotations;

namespace IssueTrackerAPI.Controllers
{
    /// <summary>
    /// Controller for managing Employee-related operations in the Issue Tracker API. ID: 00015657
    /// </summary>
    /// <remarks>
    /// Provides endpoints for CRUD operations on Employee entities.
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeesController"/> class.
        /// </summary>
        /// <param name="employeeRepository">Repository for accessing Employee data.</param>
        /// <param name="mapper">Mapper for transforming entities to DTOs and vice versa.</param>
        public EmployeesController(IRepository<Employee> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        // GET: api/Employees
        /// <summary>
        /// Retrieves all employees from the database.
        /// </summary>
        /// <returns>List of employees as DTOs.</returns>
        /// <response code="200">Returns all employees.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [SwaggerResponse(200, "Returns All Employees", typeof(IEnumerable<EmployeeDTO>))]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeDbSet()
        {
            var employees = await _employeeRepository.GetAllAsync();
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
            return Ok(employeeDtos);
        }

        // GET: api/Employees/5
        /// <summary>
        /// Retrieves a specific employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <returns>The employee as a DTO.</returns>
        /// <response code="200">Returns the requested employee.</response>
        /// <response code="404">Employee not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Returns Employee", typeof(EmployeeDTO))]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var employeeDto = _mapper.Map<EmployeeDTO>(employee);
            return Ok(employeeDto);
        }

        // PUT: api/Employees/5
        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        /// <param name="id">The ID of the employee to update.</param>
        /// <param name="employeeDto">The updated employee data.</param>
        /// <returns>No content.</returns>
        /// <response code="204">Update successful.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("{id}")]
        [SwaggerResponse(204, "Returns No Content")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeDTO employeeDto)
        {
            if (id != employeeDto.Id)
            {
                return BadRequest();
            }

            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.UpdateAsync(employee);
            return NoContent();
        }

        // POST: api/Employees
        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="employeeDto">The data for the new employee.</param>
        /// <returns>The created employee as a DTO.</returns>
        /// <response code="201">Employee created successfully.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [SwaggerResponse(201, "Returns Created")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult<Employee>> PostEmployee(EmployeeDTO employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.CreateAsync(employee);
            var createdEmployeeDto = _mapper.Map<EmployeeDTO>(employee);
            return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployeeDto.Id }, createdEmployeeDto);
        }

        // DELETE: api/Employees/5
        /// <summary>
        /// Deletes an employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>No content.</returns>
        /// <response code="204">Delete successful.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Employee not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Returns No Content")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            await _employeeRepository.DeleteAsync(id);
            return NoContent();
        }

    }
}
