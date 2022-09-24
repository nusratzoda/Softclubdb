using Dapper;
using Domain;
using Domain.DataContext;

namespace Infrastructure;

public class StudentServices
{
    private DataContext _context;

    public StudentServices(DataContext context)
    {
        _context = context;
    }
    public async Task<Response<List<Student>>> GetStudent()
    {
        await using var connection = _context.CreateConnection();

        var response = await connection.QueryAsync<Student>($"select * from Student;");
        return new Response<List<Student>>(response.ToList());

    }
    public async Task<Response<Student>> AddStudent(Student student)
    {

        using var connection = _context.CreateConnection();
        {
            try
            {

                var sql = $"Insert into Student (FirstName,LastName,Email,Phone,Adress,City) VAlUES (@FirstName,@LastName,@Email,@Phone,@Adress,@City) returning id";
                var result = await connection.ExecuteScalarAsync<int>(sql, new { student.FirstName, student.LastName, student.Email, student.Phone, student.Adress, student.City });
                student.Id = result;
                return new Response<Student>(student);
            }

            catch (Exception ex)
            {
                return new Response<Student>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
    public async Task<Response<Student>> DeleteStudent(int id)
    {

        using var connection = _context.CreateConnection();
        {
            string sql = $"delete from Student where Id = '{id}';";
            try
            {
                var response = await connection.ExecuteAsync(sql);
                return new Response<Student>(System.Net.HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                return new Response<Student>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
    public async Task<Response<Student>> UpdateStudent(Student student)
    {
        using var connection = _context.CreateConnection();
        {
            string sql = $"UPDATE Student SET FirstName = '{student.FirstName}', LastName = '{student.LastName}',Email = '{student.Email}',Phone = '{student.Phone}',Adress = '{student.Adress}',City = '{student.City}'  WHERE Id = {student.Id}; ";
            try
            {
                var response = await connection.ExecuteAsync(sql);
                return new Response<Student>(System.Net.HttpStatusCode.OK, "Success");
            }
            catch (Exception ex)
            {
                return new Response<Student>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }


}
