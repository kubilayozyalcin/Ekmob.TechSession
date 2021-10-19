using Ekmob.TechSession.Consumer.Entities;
using Ekmob.TechSession.Consumer.Services.Abstractions;
using Ekmob.TechSession.Consumer.Services.Concrete;
using Ekmob.TechSession.Shared.BaseController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Consumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : CustomBaseController
    {
        #region Variables
        private readonly ICustomerService _customerService;
        #endregion

        #region Constructor
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        #endregion

        #region Crud_Actions
        [HttpGet(Name = "GetCustomers")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _customerService.GetCustomers();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{id:length(24)}", Name = "GetCustomer")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _customerService.GetCustomer(id);
            return CreateActionResultInstance(response);
        }


        [HttpPost(Name = "CreateCustomer")]
        public async Task<IActionResult> Create(Customer customer)
        {
            var response = await _customerService.Create(customer);
            return CreateActionResultInstance(response);
        }
        #endregion

    }
}
