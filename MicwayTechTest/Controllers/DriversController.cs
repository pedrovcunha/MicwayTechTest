using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicwayTechTest.Contracts;
using MicwayTechTest.Models;
using Newtonsoft.Json;

namespace MicwayTechTest.Controllers
{
    [Produces("application/json")]
    [Route("api/Drivers")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        public delegate object DateTimeDriverDob(string key, object value);

        private readonly IDriverRepository _customerRepository;

        public DriversController(IDriverRepository driverRepository)
        {
            _customerRepository = driverRepository;
        }

        private async Task<bool> DriverExists(int id)
        {
            return await _customerRepository.Exist(id);
        }

        [HttpGet]
        [Produces(typeof(DbSet<Driver>))]
        [ResponseCache(Duration = 60)]
        public IActionResult GetDriver()
        {
            var results = new ObjectResult(_customerRepository.GetAll())
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            Request.HttpContext.Response.Headers.Add("X-Total-Count", _customerRepository.GetAll().Count().ToString());

            return results;
        }

        [HttpGet("{id}")]
        [Produces(typeof(Driver))]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetDriver([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var driver = await _customerRepository.Find(id);
            
            if (driver == null)
            {
                return NotFound();
            }

            return Ok(driver);
            
        }

       
        [HttpPost]
        [Produces(typeof(Driver))]
        public async Task<IActionResult> PostDriver([FromBody] Driver driver)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // if driver id is not set at the json.
            if (driver.Id == 0)
            {
                int totalDrivers = _customerRepository.GetAll().Count();
                driver.Id = totalDrivers + 1;
            }


            await _customerRepository.Add(driver);

            return CreatedAtAction("GetDriver", new { id = driver.Id }, driver);
        }

        [HttpPut("{id}")]
        [Produces(typeof(Driver))]
        public async Task<IActionResult> PutDriver([FromRoute] int id, [FromBody] Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != driver.Id)
            {
                return BadRequest();
            }
            
            try
            {
                await _customerRepository.Update(driver);
                return Ok(driver);
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!await DriverExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
        }

        [HttpDelete("{id}")]
        [Produces(typeof(Driver))]
        public async Task<IActionResult> DeleteDriver([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            

            if (! await DriverExists(id))
            {
                return NotFound();
            }
            await _customerRepository.Remove(id);

            return Ok();
        }

    }
}