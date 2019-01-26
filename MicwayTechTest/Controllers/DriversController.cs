using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        private readonly DriversRDSStorageDbContext _context;

        public DriversController(DriversRDSStorageDbContext context)
        {
            _context = context;
        }

        private bool DriverExists (int id)
        {
            return _context.Driver.Any(c => c.Id == id);
        }

        [HttpGet]
        [Produces(typeof(DbSet<Driver>))]
        public IActionResult GetDriver()
        {
            var results = new ObjectResult(_context.Driver)
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            Request.HttpContext.Response.Headers.Add("X-Total-Count", _context.Driver.Count().ToString());

            return results;
        }

        [HttpGet("{id}")]
        [Produces(typeof(Driver))]
        public async Task<IActionResult> GetDriver([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var driver = await _context.Driver.SingleOrDefaultAsync(m => m.Id == id);
            
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
                int totalDrivers = _context.Driver.Count();
                driver.Id = totalDrivers + 1;
            }


            _context.Driver.Add(driver);
            await _context.SaveChangesAsync();
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
            _context.Entry(driver).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return Ok(driver);
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!!DriverExists(id))
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

            var driver = await _context.Driver.SingleOrDefaultAsync(m => m.Id == id);

            if (driver == null)
            {
                return NotFound();
            }
            _context.Driver.Remove(driver);
            await _context.SaveChangesAsync();
            return Ok(driver);
        }

    }
}