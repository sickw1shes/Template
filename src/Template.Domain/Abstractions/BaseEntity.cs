using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Template.Domain.Constants;

namespace Template.Domain.Abstractions;

public abstract class BaseEntity
{
	[Key]
	[Column(Columns.Id)]
	public long Id { get; set; }
}
