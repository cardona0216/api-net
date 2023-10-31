using CustomersApi.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CustomersApi.Repositories
{
    public class CustomerDatabaseContext : DbContext
    {
        public CustomerDatabaseContext(DbContextOptions<CustomerDatabaseContext> options) : base(options)
        {
            ///constructor
        }
        //con esto hacemos referencia a las tablas en la basa de datos
        public DbSet<CustomerEntity> Customers { get; set; }

        public async Task<CustomerEntity?> Get(long id)
        {
            return await Customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Delete(long id)
        {
            CustomerEntity entity = await Get(id);
            Customers.Remove(entity);
            SaveChanges();
            return true;
        }

        public async Task<CustomerEntity> Add(CreateCustomerDto customerDto)
        {
            CustomerEntity entity = new CustomerEntity()
            {
                Id = null,
                FirstName = customerDto.FirstName,
                LastName= customerDto.LastName,
                Email = customerDto.Email,
                Address = customerDto.Address,
                Phone = customerDto.Phone

            };
               EntityEntry<CustomerEntity> response = await Customers.AddAsync(entity);
               await SaveChangesAsync();
               return await Get(response.Entity.Id ?? throw new Exception("no se ha podido guardar "));
        }

        public async Task<bool> Actualizar(CustomerEntity customerEntity)
        {
            Customers.Update(customerEntity);
            await SaveChangesAsync();
            return true;
        }

        
    }


    public class CustomerEntity
    {
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }


        public CustomerDto ToDto()
        {
            return new CustomerDto()
            {
                Address = Address,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Phone = Phone,
                Id = Id ?? throw new Exception("El id no puede ser null")

            };
        }


    }
}
