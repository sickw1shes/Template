namespace Template.Persistence.Common;

public interface IAuditUserProvider
{
	long? GetUserId();
	string GetUserRole();
}
