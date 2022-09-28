using Domain;

namespace Infrastructure.Services;

public interface IGroupServices
{
    Task<Response<List<Groupes>>> GetGroup();
    Task<Response<Groupes>> AddGroup(Groupes group);
    Task<Response<Groupes>> DeleteGroup(int id);
    Task<Response<Groupes>> UpdateGroup(Groupes group);
}
