using Dapper;
using Domain;
using Domain.DataContext;

namespace Infrastructure.Services;

public class MentorServices
{
    private DataContext _context;
    public MentorServices(DataContext context)
    {
        _context = context;
    }
    public async Task<Response<List<Mentor>>> GetMentor()
    {
        await using var connection = _context.CreateConnection();

        var response = await connection.QueryAsync<Mentor>($"select * from Mentor;");
        return new Response<List<Mentor>>(response.ToList());
    }
    public async Task<Response<Mentor>> AddMentor(Mentor mentor)
    {
        using var connection = _context.CreateConnection();
        {
            try
            {
                var sql = $"Insert into Mentor (FirstName,LastName,Email,Phone,Address,City) VAlUES (@FirstName,@LastName,@Email,@Phone,@Address,@City) returning id";
                var result = await connection.ExecuteScalarAsync<int>(sql, new { mentor.FirstName, mentor.LastName, mentor.Email, mentor.Phone, mentor.Address, mentor.City });
                mentor.Id = result;
                return new Response<Mentor>(mentor);
            }
            catch (Exception ex)
            {
                return new Response<Mentor>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
    public async Task<Response<Mentor>> DeleteMentor(int id)
    {
        using var connection = _context.CreateConnection();
        {
            string sql = $"delete from Mentor where Id = '{id}';";
            try
            {
                var response = await connection.ExecuteAsync(sql);
                return new Response<Mentor>(System.Net.HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                return new Response<Mentor>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
    public async Task<Response<Mentor>> UpdateMentor(Mentor mentor)
    {
        using var connection = _context.CreateConnection();
        {
            string sql = $"UPDATE Mentor SET FirstName = '{mentor.FirstName}', LastName = '{mentor.LastName}',Email = '{mentor.Email}',Phone = '{mentor.Phone}',Adress = '{mentor.Address}',City = '{mentor.City}'  WHERE Id = {mentor.Id}; ";
            try
            {
                var response = await connection.ExecuteAsync(sql);
                return new Response<Mentor>(System.Net.HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                return new Response<Mentor>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}