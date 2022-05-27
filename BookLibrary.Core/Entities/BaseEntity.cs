using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BookLibrary.Data.Entities;

[ExcludeFromCodeCoverage]
public class BaseEntity
{
    [Key] public int Id { get; set; }
}