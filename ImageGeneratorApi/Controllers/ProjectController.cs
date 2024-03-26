using ImageGeneratorApi.Domain.clients.StableDiffusion.dto;
using ImageGeneratorApi.Domain.Project.Dto;
using ImageGeneratorApi.Domain.Project.Entities;
using ImageGeneratorApi.Domain.Project.Services;
using ImageGeneratorApi.Infrastructure.Clients;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace ImageGeneratorApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;
    private readonly IStableDifussionClient _stableDifussionClient;

    public ProjectController(IProjectService projectService, IStableDifussionClient stableDifussionClient)
    {
        _projectService = projectService;
        _stableDifussionClient = stableDifussionClient;
    }
    
    [HttpGet("{userId}")]
    [SwaggerOperation(Summary = "Get all Project of a user", Description = "Retrieve all Projects from User.")]
    [SwaggerResponse(200, "Success", typeof(IEnumerable<Project>))]
    [SwaggerResponse(500, "Internal Server Error", typeof(string))]
    public Task<IActionResult> GetAllByUser(string id)
    {
        var response = _projectService.GetAllByUser(id);
        return Task.FromResult<IActionResult>(Ok(response));
    }
    
    [HttpPost("{userId}")]
    [SwaggerOperation(Summary = "Create User Project", Description = "Create Project.")]
    [SwaggerResponse(200, "Success")]
    [SwaggerResponse(400, "Bad Request", typeof(string))]
    [SwaggerResponse(500, "Internal Server Error", typeof(string))]
    [Authorize]
    public async Task<IActionResult> Create(string userId, [FromBody] ProjectDto projectRequest)
    {
        if (projectRequest == null || string.IsNullOrEmpty(projectRequest.Name) 
                                   || string.IsNullOrEmpty(projectRequest.Description)
                                   || string.IsNullOrEmpty(userId))
        {
            return BadRequest("Invalid project");
        }

        try
        {
            await _projectService.CreateProjectAsync(userId, projectRequest);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
    }
    
    //get models from stable difussion
    [HttpGet]
    [SwaggerOperation(Summary = "Get all Stable Models", Description = "Retrieve all Stable Models.")]
    [SwaggerResponse(200, "Success", typeof(IEnumerable<StableModel>))]
    [SwaggerResponse(500, "Internal Server Error", typeof(string))]
    public async Task<IActionResult> GetStableModels()
    {
        try
        {
            var response = await _stableDifussionClient.GetStableModelsAsync();
            Console.WriteLine(response);
            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
    }
    
}