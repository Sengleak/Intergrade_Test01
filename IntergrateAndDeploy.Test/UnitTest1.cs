using System;
using System.Linq;
using System.Threading.Tasks;
using IntergrateDatabase.api.Controllers;
using IntergrateDatabase.api.CustomerAndContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace IntergrateAndDeploy.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.True(1==1);
        }

        [Fact]
        public async Task CustomerIntergrationTest()
        {
            // Create DB Context
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
 
            var optionsBuilder = new DbContextOptionsBuilder<CustomerContext>();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

            var context = new CustomerContext(optionsBuilder.Options);

            //Make sure to Delete all Existing Customers in Databse
                //context.customers.RemoveRange(await context.customers.ToArrayAsync());
                //await context.SaveChangesAsync();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            // Create Controller
            var controller = new CustomerController(context);

            //Add Customer
            await controller.CreateCustomer(new Customer()
            {
                CustomerName = "Sengleak"
            });

            //Check if GetAll() return the added customer
            var result = (await controller.GetAll()).ToArray();
            Assert.Single(result);
            Assert.Equal("Sengleak", result[0].CustomerName);

        }
    }
    
}
