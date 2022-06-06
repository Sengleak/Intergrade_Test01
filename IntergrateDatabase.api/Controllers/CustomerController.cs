using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntergrateDatabase.api.CustomerAndContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IntergrateDatabase.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerContext _context;

        public CustomerController(CustomerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> GetAll()
        => await _context.customers.ToArrayAsync();

        [HttpPost]
        public async Task<Customer> CreateCustomer([FromBody] Customer input)
        {
            _context.customers.Add(input);
            await _context.SaveChangesAsync();
            return input;
        }
    }


}
