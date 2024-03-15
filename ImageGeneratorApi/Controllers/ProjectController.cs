using ImageGeneratorApi.Domain.Dto;
using ImageGeneratorApi.Domain.Services;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace ImageGeneratorApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }
    
    [HttpGet("{userId}")]
    [SwaggerOperation(Summary = "Get all Project of a user", Description = "Retrieve all Projects from User.")]
    [SwaggerResponse(200, "Success", typeof(IEnumerable<Domain.Entities.Project>))]
    [SwaggerResponse(500, "Internal Server Error", typeof(string))]
    public Task<IActionResult> GetAllByUser(string id)
    {
        var response = _projectService.GetAllByUser(id).ToList();
        return Task.FromResult<IActionResult>(Ok(response));
    }
    
    [HttpPost]
    [SwaggerOperation(Summary = "Create User Project", Description = "Create Project.")]
    [SwaggerResponse(200, "Success")]
    [SwaggerResponse(400, "Bad Request", typeof(string))]
    [SwaggerResponse(500, "Internal Server Error", typeof(string))]
    public async Task<IActionResult> Create([FromBody] ProjectDto projectRequest)
    {
        if (projectRequest == null)
        {
            return BadRequest("Invalid project");
        }

        try
        {
            await _projectService.CreateProjectAsync(projectRequest);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
    }
}