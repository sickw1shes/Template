using Microsoft.AspNetCore.Identity;
using Template.Domain.Entities.Identity;

namespace Template.Domain.Entities.Identity;

public class UserRole : IdentityUserRole<long>
{
	public virtual User User { get; set; }
	public virtual Role Role { get; set; }
}
