﻿using AutoMapper;
using Ekmob.TechSession.RabbitMQ.Events;
using Ekmob.TechSession.Producer.Entites;
using Ekmob.TechSession.Producer.Services.Abstractions;
using Ekmob.TechSession.RabbitMQ.Core;
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


        [HttpPost("CreateCustomer")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> CreateCustomer(Employee employee)
        {
            var employeeResult = await _employeeService.Create(employee);

            var departmentResult = await _departmentService.GetDepartment(employee.DepartmentId);

            if (departmentResult == null)
                return NotFound();

            if (!departmentResult.IsSuccessful)
            {
                _logger.LogError("Department can not be completed");
                return BadRequest();
            }

            CustomerCreateEvent eventMessage = _mapper.Map<CustomerCreateEvent>(employeeResult);
            eventMessage.DepartmentName = departmentResult.Data.DepartmentName;

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
