using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class StudentControler
{
    private StudentServices _studentService;
    public StudentControler(StudentServices studentService)
    {
        _studentService = studentService;
    }
    [HttpGet("GetStudent")]
    public async Task<Response<List<Student>>> GetStudent()
    {
        return await _studentService.GetStudent();
    }
    [HttpPost("AddStudent")]
    public async Task<Response<Student>> AddStudent(Student student)
    {
        return await _studentService.AddStudent(student);
    }
    [HttpPut("UpdateStudent")]
    public async Task<Response<Student>> UpdateStudent(Student student)
    {
        return await _studentService.UpdateStudent(student);
    }
    [HttpDelete("DeleteStudent")]
    public async Task<Response<Student>> DeleteStudent(int id)
    {
        return await _studentService.DeleteStudent(id);
    }
}
