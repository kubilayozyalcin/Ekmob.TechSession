using AutoMapper;
using Ekmob.TechSession.Core.Utilities.Response;
using Ekmob.TechSession.Producer.Data.Abstractions;
using Ekmob.TechSession.Producer.Entites;
using Ekmob.TechSession.Producer.Services.Abstractions;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mass = MassTransit;

namespace Ekmob.TechSession.Producer.Services.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ISourcingContext _context;
        private readonly IMapper _mapper;
        private readonly Mass.IPublishEndpoint _publishEndpoint;
        public EmployeeService(ISourcingContext context, IMapper mapper, Mass.IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Response<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _context.Employees.Find(employee => true).ToListAsync();

            if (employees.Any())
            {
                foreach (var employee in employees)
                {
                    employee.Department = await _context.Departments.Find(x => x.Id == employee.DepartmentId).FirstAsync();
                }
            }
            else
                employees = new List<Employee>();

            return Response<IEnumerable<Employee>>.Success(_mapper.Map<IEnumerable<Employee>>(employees), 200);
        }

        public async Task<Response<Employee>> GetEmployee(string id)
        {
            var employee = await _context.Employees.Find<Employee>(x => x.Id == id).FirstOrDefaultAsync();

            if (employee == null)
                return Response<Employee>.Fail("Employee not found", 404);

            employee.Department = await _context.Departments.Find(x => x.Id == employee.DepartmentId).FirstAsync();

            return Response<Employee>.Success(_mapper.Map<Employee>(employee), 200);
        }

        public async Task<Response<IEnumerable<Employee>>> GetEmployeeByDepartment(string departmentId)
        {
            var employees = await _context.Employees.Find(x => x.DepartmentId == departmentId).ToListAsync();

            if (employees.Any())
            {
                foreach (var employee in employees)
                {
                    employee.Department = await _context.Departments.Find(x => x.Id == employee.DepartmentId).FirstAsync();
                }
            }
            else
                employees = new List<Employee>();

            return Response<IEnumerable<Employee>>.Success(_mapper.Map<IEnumerable<Employee>>(employees), 200);
        }

        public async Task<Response<Employee>> Create(Employee employee)
        {
            var newEmployee = _mapper.Map<Employee>(employee);

            await _context.Employees.InsertOneAsync(employee);

            return Response<Employee>.Success(_mapper.Map<Employee>(employee), 200);
        }

        public async Task<Response<NoContent>> Update(Employee employee)
        {
            var updateEmployee = _mapper.Map<Employee>(employee);

            var result = await _context.Employees.FindOneAndReplaceAsync(x => x.Id == updateEmployee.Id, employee);

            if (result == null)
                return Response<NoContent>.Fail("Employee not found", 404);

            //await _publishEndpoint.Publish<EmployeeNameChangedEvent>(new EmployeeNameChangedEvent { Id = updateEmployee.Id, UpdatedName = updateEmployee.Name });

            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> Delete(string id)
        {
            var result = await _context.Employees.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount > 0)
                return Response<NoContent>.Success(204);
            else
                return Response<NoContent>.Fail("Employee not found", 404);
        }
    }
}
