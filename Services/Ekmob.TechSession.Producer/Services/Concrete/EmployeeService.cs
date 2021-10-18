using Ekmob.TechSession.Core.Utilities.Response;
using Ekmob.TechSession.Producer.Data.Abstractions;
using Ekmob.TechSession.Producer.Entites;
using Ekmob.TechSession.Producer.Services.Abstractions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Producer.Services.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ISourcingContext _context;
        public EmployeeService(ISourcingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees.Find(p => true).ToListAsync();
        }

        public async Task<Employee> GetEmployee(string id)
        {
            return await _context.Employees.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByDepartment(string departmentId)
        {
            var filter = Builders<Employee>.Filter.Eq(p => p.DepartmentId, departmentId);
            return await _context.Employees.Find(filter).ToListAsync();
        }

        public async Task Create(Employee employee)
        {
            await _context.Employees.InsertOneAsync(employee);
        }

        public async Task<bool> Update(Employee employee)
        {
            var updateResult = await _context.Employees.ReplaceOneAsync(g => g.Id == employee.Id, employee);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            var filter = Builders<Employee>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context.Employees.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
