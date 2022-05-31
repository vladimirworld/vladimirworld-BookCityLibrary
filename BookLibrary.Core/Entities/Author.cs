using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BookLibrary.Data.Entities;

[ExcludeFromCodeCoverage]
[Table("Authors")]
public class Author : BaseEntity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FullName
    {
        get { return $"{FirstName} {LastName}"; }
    }

    public string Bio { get; set; }

    public virtual IList<Book>? Books { get; set; }
}