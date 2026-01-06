using System.Linq.Expressions;
using DevHabit.Api.DTOs.Tags;
using DevHabit.Api.Entities;
using Microsoft.AspNetCore.StaticAssets;

namespace DevHabit.Api.DTOs.Tags;

internal static class TagsQueries
{
    public static Expression<Func<Tag, TagDto>> ProjectToDto()
    {
        return t => new TagDto
        {
            Id = t.Id,
            Name = t.Name,
            Description = t.Description,
            CreatedAtUtc = t.CreatedAtUtc,
            UpdatedAtUtc = t.UpdatedAtUtc
        };
    }
}