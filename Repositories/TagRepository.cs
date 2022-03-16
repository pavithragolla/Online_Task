using Dapper;
using Online.Models;
using task.Repositories;

namespace Online.Repositories;

public interface ITagRepository
{
     Task<Tag> Create(Tag Item);
    Task<Tag> GetById(int Id);
    Task<List<Tag>> GetList();
     Task<List<Tag>> GetAllForProduct(int Id);
}
public class TagRepository : BaseRepository, ITagRepository
{
    public TagRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<Tag> Create(Tag Item)
    {
        var query = $@"INSERT INTO public.tag(name, product_id)
         VALUES (@Name, @ProductId) RETURNING * ";

      using (var connection = NewConnection)
        {
            var res = await connection.QuerySingleOrDefaultAsync<Tag>(query, Item);
            return res;
        }
    }

    public async Task<List<Tag>> GetAllForProduct(int Id)
    {
        var query = $@"SELECT * FROM tag WHERE id = @Id";
        using (var con = NewConnection)
            return (await con.QueryAsync<Tag>(query, new { Id })).AsList();
    }

    public async Task<Tag> GetById(int Id)
    {
         var query = $@"SELECT * FROM tag WHERE id = @Id";
        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Tag>(query, new { Id });
    }

    public async Task<List<Tag>> GetList()
    {
      var query = $@"SELECT * FROM Tag";
        List<Tag> response;
        using (var con = NewConnection)
            response = (await con.QueryAsync<Tag>(query)).AsList();
        return response;
    }
}