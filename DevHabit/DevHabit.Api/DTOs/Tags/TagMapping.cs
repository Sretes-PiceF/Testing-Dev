using DevHabit.Api.DTOs.Tags;
using DevHabit.Api.Entities;

namespace DevHabit.Api.DTOs.Tags;

internal static class TagMappings
{
    public static TagDto TagDto(this Tag tag)
    {
        return new TagDto
        {
            Id = tag.Id,
            Name = tag.Name,
            Description = tag.Description,
            CreatedAtUtc = tag.CreatedAtUtc,
            UpdatedAtUtc = tag.UpdatedAtUtc
        };
    }

    public static Tag ToEntity(this CreateTagDto dto)
    {
        Tag tag = new()
        {
            Id = $"tag_{Guid.CreateVersion7()}",
            Name = dto.Name,
            Description = dto.Description,
            CreatedAtUtc = DateTime.UtcNow
        };

        return tag;
    }

    public static void UpdateFormDto(this Tag tag, UpdateTagDto dto)
    {
        tag.Name = dto.Name;
        tag.Description = dto.Description;
        tag.UpdatedAtUtc = DateTime.UtcNow;
    }
}