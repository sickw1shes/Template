using Template.Domain.Abstractions;
using Template.Domain.Attributes;
using Template.Domain.Constants;
using Template.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Template.Domain.Entities;

[Table(Tables.UserProfiles), Auditable]
public class UserProfile : BaseEntity
{
    [Key]
    [Column(Columns.Id)]
    [ForeignKey(nameof(User))]
    public new long Id { get; set; }
    public virtual User User { get; set; }

    [Column(Columns.Username)]
    public string Username { get; set; }

    [Column(Columns.FirstName)]
    public string FirstName { get; set; }

    [Column(Columns.LastName)]
    public string LastName { get; set; }

}
