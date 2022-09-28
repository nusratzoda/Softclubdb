using Domain;

namespace Infrastructure.Services;

public interface IMentorServices
{
    Task<Response<List<Mentor>>> GetMentor();
    Task<Response<Mentor>> AddMentor(Mentor mentor);
    Task<Response<Mentor>> DeleteMentor(int id);
    Task<Response<Mentor>> UpdateMentor(Mentor mentor);
}
