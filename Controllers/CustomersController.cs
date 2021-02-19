using System.Collections.Generic;
using AspNetCore.Swagger.WebApi.Models;
using AspNetCore.Swagger.WebApi.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Swagger.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IDataRepository<Customer> _dataRepository;
        public CustomersController(IDataRepository<Customer> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        /// <summary>
        /// Retrieves all customers
        /// </summary>
        /// <returns>All customers</returns>
        /// <response code="200">Returns all customers</response>
        /// <remarks> The API returns all customers</remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCustomers()
        {
            IEnumerable<Customer> customers = _dataRepository.GetAll();
            return Ok(customers);
        }


        /// <summary>
        /// Retrieves a specific customer by id.
        /// </summary>
        /// <param name="id"></param>  
        /// <returns>A customer</returns>
        /// <response code="200">Returns the newly created customer</response>
        /// <response code="404">If the customer is not found</response> 
        [HttpGet("{id}", Name = "GetCustomers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCustomers(long id)
        {
            Customer customer = _dataRepository.Get(id);
            if (customer == null)
            {
                return NotFound("The customer record couldn't be found.");
            }
            return Ok(customer);
        }

        /// <summary>
        /// Creates a customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>A newly created customer</returns>
        /// <response code="201">Returns the newly created customer</response>
        /// <response code="400">If the customer is null</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("customer is null.");
            }
            _dataRepository.Add(customer);

            return CreatedAtRoute(
                  "GetCustomers",
                  new { Id = customer.Id },
                  customer);
        }

        /// <summary>
        /// Update a specific customer by id
        /// </summary>
        // PUT api/customers/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(long id, [FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("customer is null.");
            }
            Customer customerToUpdate = _dataRepository.Get(id);
            if (customerToUpdate == null)
            {
                return NotFound("The customer record couldn't be found.");
            }
            _dataRepository.Update(customerToUpdate, customer);
            return NoContent();
        }

        /// <summary>
        /// Deletes a specific customer by id.
        /// </summary>
        /// <param name="id"></param>  
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(long id)
        {
            Customer customer = _dataRepository.Get(id);
            if (customer == null)
            {
                return NotFound("The customer record couldn't be found.");
            }
            _dataRepository.Delete(customer);
            return NoContent();
        }
    }
}
