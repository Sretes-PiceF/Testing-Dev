using DevHabit.Api.Database;
using DevHabit.Api.DTOs.Tags;
using DevHabit.Api.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevHabit.Api.Controllers;

[ApiController]
[Route("tags")]
public sealed class TagsController(ApplicationDBContext dBContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<TagsCollectionDto>> GetTags()
    {
        List<TagDto> tags = await dBContext
        .Tags
        .Select(TagsQueries.ProjectToDto())
        .ToListAsync();

        var tagsCollectionDto = new TagsCollectionDto
        {
            Data = tags
        };
        return Ok(tagsCollectionDto);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<TagDto>> GetTag(string id)
    {
        TagDto? tag = await dBContext
            .Tags
            .Where(t => t.Id == id)
            .Select(TagsQueries.ProjectToDto())
            .FirstOrDefaultAsync();

        if (tag == null)
        {
            return NotFound();
        }

        return Ok(tag);
    }


    [HttpPost]
    public async Task<ActionResult<TagDto>> CreateTag(CreateTagDto createTagDto)
    {
        Tag tag = createTagDto.ToEntity();

        dBContext.Tags.Add(tag);

        await dBContext.SaveChangesAsync();

        TagDto tagDto = tag.TagDto();

        return CreatedAtAction(
            nameof(GetTag),
            new { id = tagDto.Id },
            tagDto);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTag(string id, UpdateTagDto updateTagDto)
    {
        Tag? tag = await dBContext.Tags.FirstOrDefaultAsync(t => t.Id == id);

        if (tag == null)
        {
            return NotFound();
        }

        tag.UpdateFormDto(updateTagDto);

        await dBContext.SaveChangesAsync();

        return NoContent();
    }


    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchTag(string id, JsonPatchDocument<TagDto> patchDocument)
    {
        Tag? tag = await dBContext.Tags.FirstOrDefaultAsync(t => t.Id == id);

        if (tag is null)
        {
            return NotFound();
        }

        TagDto tagDto = tag.TagDto();

        patchDocument.ApplyTo(tagDto, ModelState);

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }
        tag.Name = tagDto.Name;
        tag.Description = tagDto.Description;
        tag.UpdatedAtUtc = DateTime.UtcNow;

        await dBContext.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTag(string id)
    {
        Tag? tag = await dBContext.Tags.FirstOrDefaultAsync(t => t.Id == id);

        if (tag is null)
        {
            return NotFound();
        }

        dBContext.Tags.Remove(tag);

        await dBContext.SaveChangesAsync();

        return NoContent();
    }
}