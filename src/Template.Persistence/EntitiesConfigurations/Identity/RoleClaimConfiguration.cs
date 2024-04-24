using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Entities.Identity;

namespace Template.Persistence.EntitiesConfigurations.Identity;

public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.ToTable("identity_role_claims").HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.RoleId).HasColumnName("role_id");
        builder.Property(p => p.ClaimType).HasColumnName("claim_type");
        builder.Property(p => p.ClaimValue).HasColumnName("claim_value");
    }
}
