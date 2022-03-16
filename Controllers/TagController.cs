using Microsoft.AspNetCore.Mvc;
using online.DTOs;
using online.Models;
using online.Repositories;
using Online.DTOs;
using Online.Models;
using Online.Repositories;

namespace online.Controllers;

[ApiController]
[Route("api/tag")]

public class TagController : ControllerBase
{
    private readonly ILogger<TagController> _logger;
    private readonly ITagRepository _Tag;

    public TagController(ILogger<TagController> logger, ITagRepository Tag)
    {
        _logger = logger;
        _Tag = Tag;
    }

    [HttpPost]
    public async Task<ActionResult<TagDTO>> CreateTag([FromBody] TagCreateDTO Data)
    {
        var ToCreateTag = new Tag
        {
            Name = Data.Name,
            ProductId = Data.ProductId
        };
        var CreatedTag = await _Tag.Create(ToCreateTag);

        return StatusCode(StatusCodes.Status201Created, CreatedTag.asDto);
    }

    [HttpGet]
    public async Task<ActionResult<List<TagDTO>>> GetAllUser()
    {
        var usersList = await _Tag.GetList();


        var dtoList = usersList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{Tag_id}")]

    public async Task<ActionResult<TagDTO>> GetUserById([FromRoute] int Tag_id)
    {

        var Tag = await _Tag.GetById(Tag_id);

        if (Tag is null)
            return NotFound("No Tag found with given Tag_id");


        var dto = Tag.asDto;
        // dto.Schedule = await _schedule.GetAllForTag(Tag.TagId);
        return Ok(dto);
    }
}
