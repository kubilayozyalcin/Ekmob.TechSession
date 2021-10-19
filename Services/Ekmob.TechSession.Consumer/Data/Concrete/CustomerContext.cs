using Ekmob.TechSession.Consumer.Data.Abstractions;
using Ekmob.TechSession.Consumer.Entities;
using Ekmob.TechSession.Consumer.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Consumer.Data.Concrete
{
    public class CustomerContext : ICustomerContext
    {
        public CustomerContext(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Customers = database.GetCollection<Customer>(settings.CustomerCollectionName);

        }

        public IMongoCollection<Customer> Customers { get; }
    }
}
