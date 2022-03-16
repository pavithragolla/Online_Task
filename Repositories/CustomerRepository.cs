using Dapper;
using online.Models;
using task.Repositories;

namespace online.Repositories;

public interface ICustomerRepository
{
    Task<Customer> Create(Customer Item);
    Task<bool> Update(Customer Item);
    Task<Customer> GetById(int Id);
    Task<List<Customer>> GetList();
}

public class CustomerRepository : BaseRepository, ICustomerRepository
{
    public CustomerRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<Customer> Create(Customer Item)
    {
        var query = $@"INSERT INTO public.customer(
	 name, address, mobile, email)
	VALUES (@Name, @Address, @Mobile, @Email) RETURNING * ";
        using (var connection = NewConnection)
        {
            var res = await connection.QuerySingleOrDefaultAsync<Customer>(query, Item);
            return res;
        }
    }

    public async Task<Customer> GetById(int Id)
    {
        var query = $@"SELECT * FROM customer WHERE id = @Id";
        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Customer>(query, new { Id });

    }

    public async Task<List<Customer>> GetList()
    {
        var query = $@"SELECT * FROM Customer";
        List<Customer> response;
        using (var con = NewConnection)
            response = (await con.QueryAsync<Customer>(query)).AsList();
        return response;
    }

    public async Task<bool> Update(Customer Item)
    {
        var query = $@"UPDATE public.customer
	SET address= @Address, mobile= @Mobile, email=@Email
	WHERE id = @Id";
        using (var connection = NewConnection)
        {
            var Count = await connection.ExecuteAsync(query, Item);
            return Count == 1;
        }
    }
}
