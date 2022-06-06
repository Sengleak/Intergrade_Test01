using System;
using Microsoft.EntityFrameworkCore;

namespace IntergrateDatabase.api.CustomerAndContext
{
    public class Customer
    {
        public int id { get; set; }
        public string CustomerName { get; set; }
    }

    public class CustomerContext: DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options ) :base(options)
        {

        }

        public DbSet<Customer> customers { get; set; }

    }
}
