using System.Collections.Generic;
using System.Linq;
using AspNetCore.Swagger.WebApi.Models.Repository;

namespace AspNetCore.Swagger.WebApi.Models.Data
{
    public class CustomerManager : IDataRepository<Customer>
    {
        readonly CustomerContext _CustomerContext;

        public CustomerManager(CustomerContext context)
        {
            _CustomerContext = context;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _CustomerContext.customers.ToList();
        }

        public Customer Get(long id)
        {
            return _CustomerContext.customers.FirstOrDefault(e => e.Id == id);
        }

        public void Add(Customer entity)
        {
            _CustomerContext.customers.Add(entity);
            _CustomerContext.SaveChanges();
        }

        public void Update(Customer customer, Customer entity)
        {
            customer.CustomerName = entity.CustomerName;            
            _CustomerContext.SaveChanges();
        }

        public void Delete(Customer customer)
        {
            _CustomerContext.customers.Remove(customer);
            _CustomerContext.SaveChanges();
        }
    }
}