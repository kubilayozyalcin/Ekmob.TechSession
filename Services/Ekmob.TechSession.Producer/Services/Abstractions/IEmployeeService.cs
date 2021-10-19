using Ekmob.TechSession.Core.Utilities.Response;
using Ekmob.TechSession.Producer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Producer.Services.Abstractions
{
    public interface IEmployeeService
    {

        Task<Response<IEnumerable<Employee>>> GetEmployees();

        Task<Response<Employee>> GetEmployee(string id);

        Task<Response<IEnumerable<Employee>>> GetEmployeeByDepartment(string departmentId);

        Task<Response<Employee>> Create(Employee employee);

        Task<Response<NoContent>> Update(Employee employee);

        Task<Response<NoContent>> Delete(string id);

    }
}
