using Domain;

namespace Infrastructure.Services;

public interface IStudentServices
{
    Task<Response<List<Student>>> GetStudent();
    Task<Response<Student>> AddStudent(Student student);
    Task<Response<Student>> DeleteStudent(int id);
    Task<Response<Student>> UpdateStudent(Student student);
}
