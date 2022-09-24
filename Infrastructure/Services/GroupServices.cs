using Dapper;
using Domain;
using Domain.DataContext;
namespace Infrastructure.Services;
public class GroupServices
{
    private DataContext _context;
    public GroupServices(DataContext context)
    {
        _context = context;
    }
    public async Task<Response<List<Group>>> GetGroup()
    {
        await using var connection = _context.CreateConnection();
        var response = await connection.QueryAsync<Group>($"select * from Group;");
        return new Response<List<Group>>(response.ToList());
    }
    public async Task<Response<Group>> AddGroup(Group group)
    {
        using var connection = _context.CreateConnection();
        {
            try
            {
                var sql = $"Insert into Group (GroupName,GroupDescription,CourseId) VAlUES (@GroupName,@GroupDescription,@CourseId) returning id";
                var result = await connection.ExecuteScalarAsync<int>(sql, new { group.GroupName, group.GroupDescription, group.CourseId });
                group.Id = result;
                return new Response<Group>(group);
            }
            catch (Exception ex)
            {
                return new Response<Group>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
    public async Task<Response<Group>> DeleteGroup(int id)
    {
        using var connection = _context.CreateConnection();
        {
            string sql = $"delete from Group where Id = '{id}';";
            try
            {
                var response = await connection.ExecuteAsync(sql);
                return new Response<Group>(System.Net.HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                return new Response<Group>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
    public async Task<Response<Group>> UpdateGroup(Group group)
    {
        using var connection = _context.CreateConnection();
        {
            string sql = $"UPDATE Group SET GroupName = '{group.GroupName}', GroupDescription = '{group.GroupDescription}',CourseId = '{group.CourseId}'; ";
            try
            {
                var response = await connection.ExecuteAsync(sql);
                return new Response<Group>(System.Net.HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                return new Response<Group>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
