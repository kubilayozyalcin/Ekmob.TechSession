using Ekmob.TechSession.Producer.Dtos;
using Ekmob.TechSession.Producer.Entites;
using Ekmob.TechSession.Shared.Utilities.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Producer.Services.Abstractions
{
    public interface IEmployeeService
    {

        Task<Response<List<EmployeeDto>>> GetEmployees();
        Task<Response<EmployeeDto>> GetEmployee(string id);
        Task<Response<List<EmployeeDto>>> GetEmployeeByDepartment(string departmentId);
        Task<Response<EmployeeDto>> Create(EmployeeCreateDto employee);
        Task<Response<NoContent>> Update(EmployeeUpdateDto employee);
        Task<Response<NoContent>> Delete(string id);

    }
}
