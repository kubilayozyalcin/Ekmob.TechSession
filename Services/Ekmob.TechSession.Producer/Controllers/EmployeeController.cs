using AutoMapper;
using Ekmob.TechSession.Producer.Dtos;
using Ekmob.TechSession.Producer.Services.Abstractions;
using Ekmob.TechSession.RabbitMQ.Core;
using Ekmob.TechSession.RabbitMQ.Events;
using Ekmob.TechSession.Shared.BaseController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : CustomBaseController
    {
        #region Variables
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;
        private readonly EventBusRabbitMQProducer _eventBus;
        #endregion

        #region Constructor
        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService,
            EventBusRabbitMQProducer eventBus, ILogger<EmployeeController> logger, IMapper mapper)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _logger = logger;
            _mapper = mapper;
            _eventBus = eventBus;
        }
        #endregion

        #region Crud_Actions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _employeeService.GetEmployees();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _employeeService.GetEmployee(id);
            return CreateActionResultInstance(response);
        }


        [HttpGet]
        [Route("/api/[controller]/GetAllByDepartmentId/{departmentId}")]
        public async Task<IActionResult> GetAllByDepartmentId(string departmentId)
        {
            var response = await _employeeService.GetEmployeeByDepartment(departmentId);
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateDto employeeCreateDto)
        {
            var response = await _employeeService.Create(employeeCreateDto);

            if (response.IsSuccessful)
            {
                var rabbitMQ = CheckCustomer(response.Data.Id);
            }

            return CreateActionResultInstance(response);

        }

        [HttpPut]
        public async Task<IActionResult> Update(EmployeeUpdateDto employeeUpdateDto)
        {
            var response = await _employeeService.Update(employeeUpdateDto);
            return CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _employeeService.Delete(id);
            return CreateActionResultInstance(response);
        }


        [HttpPost("CreateCustomerRabbitMQ")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> CheckCustomer(string id)
        {
            var employeeResult = await _employeeService.GetEmployee(id);

            var departmentResult = await _departmentService.GetDepartment(employeeResult.Data.DepartmentId);

            if (departmentResult == null)
                return NotFound();

            if (!departmentResult.IsSuccessful)
            {
                _logger.LogError("Department can not be completed");
                return BadRequest();
            }

            //CustomerCreateEvent eventMessage = _mapper.Map<CustomerCreateEvent>(employeeResult);
            CustomerCreateEvent eventMessage = new CustomerCreateEvent();
            eventMessage.DepartmentName = departmentResult.Data.DepartmentName;
            eventMessage.Name = employeeResult.Data.Name;
            eventMessage.IdentityNumber = employeeResult.Data.IdentityNumber;
            eventMessage.Email = employeeResult.Data.Email;
            eventMessage.JobTitle = employeeResult.Data.JobTitle;
            eventMessage.Id = employeeResult.Data.Id;

            try
            {
                _eventBus.Publish(EventBusConstants.CustomerCreateQueue, eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {EventId} from {AppName}", eventMessage.Id, "Ekmob");
                throw;
            }

            return Accepted();
        }
        #endregion
    }
}
