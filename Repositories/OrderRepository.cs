using Dapper;
using Online.DTOs;
using Online.Models;
using task.Repositories;

namespace Online.Repositories;

public interface IOrderRepository
{
    Task<Order> Create(Order Item);
    Task<bool> Update(Order Item);
    Task<Order> GetById(int Id);
    Task<List<Order>> GetList();
    Task<List<Order>> GetAllForCustomer(int Id);
}

public class OrderRepository : BaseRepository, IOrderRepository
{
    public OrderRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<Order> Create(Order Item)
    {
        var query = $@"INSERT INTO ""order""(
	order_date, delivery_date, customer_id)
	VALUES (@OrderDate, @DeliveryDate, @CustomerId) RETURNING * ";

        using (var connection = NewConnection)
        {
            var res = await connection.QuerySingleOrDefaultAsync<Order>(query, Item);
            return res;
        }
    }

    public async Task<List<Order>> GetAllForCustomer(int Id)
    {
        var query = $@"SELECT * FROM ""order"" WHERE customer_id = @Id";
        using (var con = NewConnection)
            return (await con.QueryAsync<Order>(query, new { Id })).AsList();
    }

    public async Task<Order> GetById(int Id)
    {
        var query = $@"SELECT * FROM ""order"" WHERE id = @Id";
        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Order>(query, new { Id });
    }

    public async Task<List<Order>> GetList()
    {
        var query = $@"SELECT * FROM ""order""";
        List<Order> response;
        using (var con = NewConnection)
            response = (await con.QueryAsync<Order>(query)).AsList();
        return response;
    }

    public async Task<bool> Update(Order Item)
    {
        var query = $@"UPDATE public.""order""
	SET delivery_date = @DeliveryDate	WHERE id = @Id ";

        using (var connection = NewConnection)
        {
            var Count = await connection.ExecuteAsync(query, Item);
            return Count == 1;
        }
    }
}