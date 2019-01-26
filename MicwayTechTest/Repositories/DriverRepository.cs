using Microsoft.EntityFrameworkCore;
using MicwayTechTest.Contracts;
using MicwayTechTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicwayTechTest.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private DriversRDSStorageDbContext _context;

        public DriverRepository(DriversRDSStorageDbContext context)
        {
            _context = context;
        }

        public async Task<Driver> Add(Driver driver)
        {
            await _context.Driver.AddAsync(driver);
            await _context.SaveChangesAsync();
            return driver;
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.Driver.AnyAsync(c => c.Id == id);
        }

        public async Task<Driver> Find(int id)
        {
            return await _context.Driver.SingleOrDefaultAsync(m => m.Id == id);
        }

        public IEnumerable<Driver> GetAll()
        {
            return _context.Driver;
        }

        public async Task<Driver> Remove(int id)
        {
            var driver = await _context.Driver.SingleAsync(a => a.Id == id);
            _context.Driver.Remove(driver);
            await _context.SaveChangesAsync();
            return driver;
        }

        public async Task<Driver> Update(Driver driver)
        {
            _context.Driver.Update(driver);
            await _context.SaveChangesAsync();
            return driver;
        }
    }
}
