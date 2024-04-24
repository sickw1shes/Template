using System.ComponentModel.DataAnnotations.Schema;
using Template.Domain.Constants;

namespace Template.Domain.Abstractions;

public abstract class AuditableEntity : BaseEntity
{
	[Column(Columns.Deleted)]
	public bool IsDeleted { get; set; }
}
