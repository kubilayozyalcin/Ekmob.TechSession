using AutoMapper;
using Ekmob.TechSession.Shared.Utilities.Response;
using Ekmob.TechSession.Producer.Data.Abstractions;
using Ekmob.TechSession.Producer.Entites;
using Ekmob.TechSession.Producer.Services.Abstractions;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ekmob.TechSession.Producer.Dtos;

namespace Ekmob.TechSession.Producer.Services.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ISourcingContext _context;
        private readonly IMapper _mapper;
        public EmployeeService(ISourcingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<List<EmployeeDto>>> GetEmployees()
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

            return Response<List<EmployeeDto>>.Success(_mapper.Map<List<EmployeeDto>>(employees), 200);
        }

        public async Task<Response<EmployeeDto>> GetEmployee(string id)
        {
            var employee = await _context.Employees.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (employee == null)
                return Response<EmployeeDto>.Fail("Employee not found", 404);

            employee.Department = await _context.Departments.Find(x => x.Id == employee.DepartmentId).FirstAsync();

            return Response<EmployeeDto>.Success(_mapper.Map<EmployeeDto>(employee), 200);
        }

        public async Task<Response<List<EmployeeDto>>> GetEmployeeByDepartment(string departmentId)
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

            return Response<List<EmployeeDto>>.Success(_mapper.Map<List<EmployeeDto>>(employees), 200);
        }

        public async Task<Response<EmployeeDto>> Create(EmployeeCreateDto employeeCreateDto)
        {
            var newEmployee = _mapper.Map<Employee>(employeeCreateDto);

            await _context.Employees.InsertOneAsync(newEmployee);

            return Response<EmployeeDto>.Success(_mapper.Map<EmployeeDto>(newEmployee), 200);
        }

        public async Task<Response<NoContent>> Update(EmployeeUpdateDto employeeUpdateDto)
        {
            var updateEmployee = _mapper.Map<Employee>(employeeUpdateDto);

            var result = await _context.Employees.FindOneAndReplaceAsync(x => x.Id == employeeUpdateDto.Id, updateEmployee);

            if (result == null)
                return Response<NoContent>.Fail("Employee not found", 404);

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
