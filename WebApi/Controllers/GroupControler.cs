using Domain;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class GroupControler : ControllerBase
{
    private IGroupServices _GroupService;
    public GroupControler(IGroupServices groupService)
    {
        _GroupService = groupService;
    }
    [HttpGet("GetGroup")]
    public async Task<Response<List<Groupes>>> GetGroup()
    {
        return await _GroupService.GetGroup();
    }
    [HttpPost("AddGroup")]
    public async Task<Response<Groupes>> AddGroup(Groupes group)
    {
        return await _GroupService.AddGroup(group);
    }
    [HttpPut("UpdateGroup")]
    public async Task<Response<Groupes>> UpdateGroup(Groupes group)
    {
        return await _GroupService.UpdateGroup(group);
    }
    [HttpDelete("DeleteGroup")]
    public async Task<Response<Groupes>> DeleteGroup(int id)
    {
        return await _GroupService.DeleteGroup(id);
    }
}
