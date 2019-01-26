using MicwayTechTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicwayTechTest.Contracts
{
    public interface IDriverRepository
    {
        Task<Driver> Add(Driver driver);

        IEnumerable<Driver> GetAll();

        Task<Driver> Find(int id);

        Task<Driver> Update(Driver driver);

        Task<Driver> Remove(int id);

        Task<bool> Exist(int id);

    }
}
