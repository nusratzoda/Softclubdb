using Dapper;
using Domain;
using Domain.DataContext;
namespace Infrastructure.Services;
public class GroupServices : IGroupServices
{
    private DataContext _context;
    public GroupServices(DataContext context)
    {
        _context = context;
    }
    public async Task<Response<List<Groupes>>> GetGroup()
    {
        await using var connection = _context.CreateConnection();

        var response = await connection.QueryAsync<Groupes>($"select * from Groupes;");
        return new Response<List<Groupes>>(response.ToList());
    }
    public async Task<Response<Groupes>> AddGroup(Groupes group)
    {
        using var connection = _context.CreateConnection();
        try
        {
            var sql = $"Insert into Groupes (GroupName,GroupDescription,CourseId) VAlUES (@GroupName,@GroupDescription,@CourseId) returning id";
            var result = await connection.ExecuteScalarAsync<int>(sql, new { group.GroupName, group.GroupDescription, group.CourseId });
            group.Id = result;
            return new Response<Groupes>(group);
        }
        catch (Exception ex)
        {
            return new Response<Groupes>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }

    }
    public async Task<Response<Groupes>> DeleteGroup(int id)
    {
        using var connection = _context.CreateConnection();
        string sql = $"delete from Groupes where Id = '{id}';";
        try
        {
            var response = await connection.ExecuteAsync(sql);
            return new Response<Groupes>(System.Net.HttpStatusCode.OK, "Success");
        }
        catch (Exception ex)
        {
            return new Response<Groupes>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<Groupes>> UpdateGroup(Groupes group)
    {
        using var connection = _context.CreateConnection();
        string sql = ($"UPDATE Groupes SET GroupName = '{group.GroupName}', GroupDescription = '{group.GroupDescription}',CourseId = '{group.CourseId}'  WHERE Id = {group.Id}; ");
        try
        {
            var response = await connection.ExecuteAsync(sql);
            return new Response<Groupes>(System.Net.HttpStatusCode.OK, "Success");
        }
        catch (Exception ex)
        {
            return new Response<Groupes>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
