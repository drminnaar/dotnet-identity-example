using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Data.Models.Configuration
{
    using static Identity.Data.Schema.SchemaInfo;
    using static Identity.Data.Schema.SchemaInfo.AppRoleClaimTable;

    internal sealed class AppRoleClaimConfiguration : IEntityTypeConfiguration<AppRoleClaim>
    {
        public void Configure(EntityTypeBuilder<AppRoleClaim> entity)
        {
            entity.ToTable(TableName, SchemaName);

            entity.HasKey(e => e.Id).HasName(Key.PrimaryKey);

            entity.Property(e => e.ClaimType).HasColumnName(Column.ClaimType);
            entity.Property(e => e.ClaimValue).HasColumnName(Column.ClaimValue);
            entity.Property(e => e.Id).HasColumnName(Column.Id);
            entity.Property(e => e.RoleId).HasColumnName(Column.RoleId);

            entity.HasIndex(e => e.RoleId).HasName(Index.RoleId);

            entity.HasOne(e => e.Role).WithMany(e => e.RoleClaims).HasForeignKey(e => e.RoleId).HasConstraintName(Key.RoleIdForeignKey);
        }
    }
}