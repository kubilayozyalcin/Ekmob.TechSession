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
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(string id);
        Task<IEnumerable<Employee>> GetEmployeeByDepartment(string departmentId);

        Task Create(Employee employee);
        Task<bool> Update(Employee employee);
        Task<bool> Delete(string id);
    }
}
