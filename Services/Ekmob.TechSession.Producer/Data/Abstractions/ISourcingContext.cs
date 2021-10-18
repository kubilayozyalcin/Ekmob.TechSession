using Ekmob.TechSession.Producer.Entites;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Producer.Data.Abstractions
{
    public interface ISourcingContext
    {
        IMongoCollection<Employee> Employees { get; }
        IMongoCollection<Department> Departments { get; }
    }
}
