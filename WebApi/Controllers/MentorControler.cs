using Domain;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class MentorControler : ControllerBase
{
    private IMentorServices _mentorService;
    public MentorControler(IMentorServices mentorService)
    {
        _mentorService = mentorService;
    }
    [HttpGet("GetMentor")]
    public async Task<Response<List<Mentor>>> GetMentor()
    {
        return await _mentorService.GetMentor();
    }
    [HttpPost("AddMentor")]
    public async Task<Response<Mentor>> AddMentor(Mentor mentor)
    {
        return await _mentorService.AddMentor(mentor);
    }
    [HttpPut("UpdateMentor")]
    public async Task<Response<Mentor>> UpdateMentor(Mentor mentor)
    {
        return await _mentorService.UpdateMentor(mentor);
    }
    [HttpDelete("DeleteMentor")]
    public async Task<Response<Mentor>> DeleteMentor(int id)
    {
        return await _mentorService.DeleteMentor(id);
    }
}
