using Ekmob.TechSession.Producer.Entites;
using Ekmob.TechSession.Producer.Services.Abstractions;
using Ekmob.TechSession.Shared.BaseController;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : CustomBaseController
    {
        #region Variables
        private readonly IEmployeeService _employeeService;
        #endregion

        #region Constructor
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion

        #region Crud_Actions
        [HttpGet(Name = "GetEmployees")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _employeeService.GetEmployees();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{id:length(24)}", Name = "GetEmployee")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _employeeService.GetEmployee(id);
            return CreateActionResultInstance(response);
        }


        [HttpGet("{id:length(24)}", Name = "GetEmployeesByDepartmentId")]
        [Route("/api/[controller]/GetAllByDepartmentId/{departmentId}")]
        public async Task<IActionResult> GetAllByDepartmentId(string departmentId)
        {
            var response = await _employeeService.GetEmployeeByDepartment(departmentId);
            return CreateActionResultInstance(response);
        }

        [HttpPost(Name = "CreateEmployee")]
        public async Task<IActionResult> Create(Employee employee)
        {
            var response = await _employeeService.Create(employee);
            return CreateActionResultInstance(response);
        }

        [HttpPut(Name = "UpdateEmployee")]
        public async Task<IActionResult> Update(Employee employee)
        {
            var response = await _employeeService.Update(employee);
            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteEmployee")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _employeeService.Delete(id);
            return CreateActionResultInstance(response);
        }
        #endregion
    }
}
