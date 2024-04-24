using Microsoft.AspNetCore.Identity;

namespace Template.Domain.Entities.Identity;

public class Role : IdentityRole<long>
{
	public virtual ICollection<User> Users { get; set; }
}
