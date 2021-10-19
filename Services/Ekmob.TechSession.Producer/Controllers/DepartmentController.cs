using Ekmob.TechSession.Producer.Entites;
using Ekmob.TechSession.Producer.Services.Abstractions;
using Ekmob.TechSession.Shared.BaseController;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : CustomBaseController
    {
        #region Variables
        private readonly IDepartmentService _departmentService;
        #endregion

        #region Constructor
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        #endregion

        #region Crud_Actions
        [HttpGet(Name = "GetDepartments")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _departmentService.GetDepartments();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{id:length(24)}", Name = "GetDepartment")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _departmentService.GetDepartment(id);
            return CreateActionResultInstance(response);
        }


        [HttpPost(Name = "CreateDepartment")]
        public async Task<IActionResult> Create(Department department)
        {
            var response = await _departmentService.Create(department);
            return CreateActionResultInstance(response);
        }

        [HttpPut(Name = "UpdateDepartment")]
        public async Task<IActionResult> Update(Department department)
        {
            var response = await _departmentService.Update(department);
            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteDepartment")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _departmentService.Delete(id);
            return CreateActionResultInstance(response);
        }
        #endregion
    }
}
