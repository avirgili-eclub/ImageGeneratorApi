using ImageGeneratorApi.Domain.Dto;
using ImageGeneratorApi.Domain.Entities;
using ImageGeneratorApi.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ImageGeneratorApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpGet("{projectId}")]
    [SwaggerOperation(Summary = "Get all Images by Project Id", Description = "Retrieve all Images from Project.")]
    [SwaggerResponse(200, "Success", typeof(IEnumerable<Image>))]
    [SwaggerResponse(400, "Bad Request", typeof(string))]
    [SwaggerResponse(500, "Internal Server Error", typeof(string))]
    [Authorize]
    public IActionResult GetAllByProjectId(int projectId)
    {
        var response = _imageService.GetAllByProjectId(projectId);
        return Ok(response);
    }

    [HttpPost("{projectId}")]
    [SwaggerOperation(Summary = "Create Image", Description = "Create Image.")]
    [SwaggerResponse(200, "Success")]
    [SwaggerResponse(400, "Bad Request", typeof(string))]
    [SwaggerResponse(500, "Internal Server Error", typeof(string))]
    [Authorize]
    public IActionResult Create(int projectId, [FromBody] ImageDto imageRequest)
    {
        if (imageRequest == null || string.IsNullOrEmpty(imageRequest.Name)
                                 || string.IsNullOrEmpty(imageRequest.Description)
                                 || string.IsNullOrEmpty(imageRequest.ImageType.ToString())
                                 || projectId == 0)
        {
            return BadRequest("Invalid image");
        }

        try
        {
            Image image = _imageService.DtoToEntity(imageRequest);
            image.ProjectId = projectId;
            _imageService.CreateAsync(image);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
    }
}