using Dapper;
using online.Models;
using Online.Models;
using task.Repositories;

namespace Online.Repositories;

public interface IProductRepository
{
    Task<Product> Create(Product Item);
    Task<bool> Update(Product Item);
    Task<Product> GetById(int Id);
    Task<List<Product>> GetList();
    Task<List<Product>> GetAllForCustomer(int Id);
    Task<List<Product>> GetAllForOrder(int Id);
   

}

public class ProductRepository : BaseRepository, IProductRepository
{
    public ProductRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<Product> Create(Product Item)
    {
       var query = $@" INSERT INTO public.product(
	name, price, order_id)
	VALUES (@Name, @Price, @OrderId) RETURNING * ";

     using (var connection = NewConnection)
        {
            var res = await connection.QuerySingleOrDefaultAsync<Product>(query, Item);
            return res;
        }
    }

    public async Task<List<Product>> GetAllForCustomer(int Id)
    {
       var query = $@"SELECT * FROM product WHERE id = @id";

        using (var con = NewConnection)
            return (await con.QueryAsync<Product>(query, new { Id })).AsList();
    }

    public async Task<List<Product>> GetAllForOrder(int Id)
    {
    //   var query = $@"SELECT * FROM order_product op LEFT JOIN
    //    product p ON p.id= op.product_id WHERE order_id = @Id";

    var query = $@"SELECT * FROM order_product op LEFT JOIN
     product p ON p.id= op.order_id WHERE product_id = @Id";
       using (var con = NewConnection)
            return (await con.QueryAsync<Product>(query, new { Id })).AsList();
    }

    public async Task<Product> GetById(int Id)
    {
        var query = $@"SELECT * FROM product WHERE id = @Id";
         using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Product>(query, new { Id });
    }

    public async Task<List<Product>> GetList()
    {
         var query = $@"SELECT * FROM product";
        List<Product> response;
        using (var con = NewConnection)
            response = (await con.QueryAsync<Product>(query)).AsList();
        return response;
    }

    public async Task<bool> Update(Product Item)
    {
       var query=$@"UPDATE public.product
	   SET price=@Price WHERE id =@Id ";
         using (var connection = NewConnection)
        {
            var Count = await connection.ExecuteAsync(query, Item);
            return Count == 1;
        }
    }
}
