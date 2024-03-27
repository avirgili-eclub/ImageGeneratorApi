using ImageGeneratorApi.Domain.Images.Dto;
using ImageGeneratorApi.Domain.Images.Entities;
using ImageGeneratorApi.Domain.Images.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ImageGeneratorApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IProjectImageService _projectImageService;

    public ImageController(IProjectImageService projectImageService)
    {
        _projectImageService = projectImageService;
    }

    [HttpGet("{projectId}")]
    [SwaggerOperation(Summary = "Get all Images by Project Id", Description = "Retrieve all Images from Project.")]
    [SwaggerResponse(200, "Success", typeof(IEnumerable<Imagen>))]
    [SwaggerResponse(400, "Bad Request", typeof(string))]
    [SwaggerResponse(500, "Internal Server Error", typeof(string))]
    public IActionResult GetAllByProjectId(int projectId)
    {
        var response = _projectImageService.GetAllByProjectId(projectId);
        return Ok(response);
    }

    [HttpPost("{projectId}")]
    [SwaggerOperation(Summary = "Create Image", Description = "Create Image.")]
    [SwaggerResponse(200, "Success", typeof(IEnumerable<Imagen>))]
    [SwaggerResponse(400, "Bad Request", typeof(string))]
    [SwaggerResponse(500, "Internal Server Error", typeof(string))]
    public IActionResult Create(int projectId, [FromBody] ImagenDto imagenRequest)
    {
        if (imagenRequest == null || string.IsNullOrEmpty(imagenRequest.Name)
                                 || string.IsNullOrEmpty(imagenRequest.Description)
                                 || string.IsNullOrEmpty(imagenRequest.ImageType.ToString())
                                 || projectId == 0)
        {
            return BadRequest("Invalid image");
        }

        try
        {
           var imageResponse = _projectImageService.CreateImages(imagenRequest, projectId);
           return Ok(imageResponse);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
    }
}