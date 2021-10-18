using Ekmob.TechSession.Producer.Entites;
using Ekmob.TechSession.Producer.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        #region Variables
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        #endregion

        #region Constructor
        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }
        #endregion

        #region Crud_Actions

        [HttpGet]
        [ProducesResponseType(typeof(Employee), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _employeeService.GetEmployees();
            return Ok(employees);
        }

        [HttpGet("{id:length(24)}", Name = "GetEmployee")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Employee), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Employee>> GetEmployee(string id)
        {
            var employee = await _employeeService.GetEmployee(id);
            if (employee == null)
            {
                _logger.LogError($"Employee with id : {id}, hasn't been found in database");
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpGet("{id:length(24)}", Name = "GetEmployeeByDepartment")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Employee), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Employee>> GetEmployeeByDepartment(string departmentId)
        {
            var employee = await _employeeService.GetEmployee(departmentId);
            if (employee == null)
            {
                _logger.LogError($"Employee with department id : {departmentId}, hasn't been found in database");
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Employee), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee employee)
        {
            await _employeeService.Create(employee);
            return CreatedAtRoute("GetEmployee", new { id = employee.Id }, employee);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Employee), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            return Ok(await _employeeService.Update(employee));
        }


        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(Employee), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteEmployeeById(string id)
        {
            return Ok(await _employeeService.Delete(id));
        }
        #endregion
    }
}
