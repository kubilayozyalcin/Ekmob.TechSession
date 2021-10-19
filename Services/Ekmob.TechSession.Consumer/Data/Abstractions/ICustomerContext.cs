using Ekmob.TechSession.Consumer.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Consumer.Data.Abstractions
{
    public interface ICustomerContext
    {
        IMongoCollection<Customer> Customers { get; }
    }
}
