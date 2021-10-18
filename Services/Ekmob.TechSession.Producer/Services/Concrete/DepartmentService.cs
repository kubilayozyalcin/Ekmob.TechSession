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
    public class DepartmentService : IDepartmentService
    {
        private readonly ISourcingContext _context;
        public DepartmentService(ISourcingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await _context.Departments.Find(p => true).ToListAsync();
        }

        public async Task<Department> GetDepartment(string id)
        {
            return await _context.Departments.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(Department department)
        {
            await _context.Departments.InsertOneAsync(department);
        }

        public async Task<bool> Update(Department department)
        {
            var updateResult = await _context.Departments.ReplaceOneAsync(g => g.Id == department.Id, department);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            var filter = Builders<Department>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context.Departments.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
      
    }
}
