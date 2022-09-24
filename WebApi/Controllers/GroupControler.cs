using Domain;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class GroupControler
{
    private GroupServices _GroupService;
    public GroupControler(GroupServices groupService)
    {
        _GroupService = groupService;
    }
    [HttpGet("GetGroup")]
    public async Task<Response<List<Group>>> GetGroup()
    {
        return await _GroupService.GetGroup();
    }
    [HttpPost("AddGroup")]
    public async Task<Response<Group>> AddGroup(Group group)
    {
        return await _GroupService.AddGroup(group);
    }
    [HttpPut("UpdateGroup")]
    public async Task<Response<Group>> UpdateGroup(Group group)
    {
        return await _GroupService.UpdateGroup(group);
    }
    [HttpDelete("DeleteGroup")]
    public async Task<Response<Group>> DeleteGroup(int id)
    {
        return await _GroupService.DeleteGroup(id);
    }
}
