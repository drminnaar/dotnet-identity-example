using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Data.Models.Configuration
{
    using static Identity.Data.Schema.SchemaInfo;
    using static Identity.Data.Schema.SchemaInfo.AppUserRoleTable;

    internal sealed class AppUserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> entity)
        {
            entity.ToTable(TableName, SchemaName);

            entity.HasKey(e => new { e.UserId, e.RoleId }).HasName(Key.PrimaryKey);

            entity.Property(e => e.RoleId).HasColumnName(Column.RoleId);
            entity.Property(e => e.UserId).HasColumnName(Column.UserId);

            entity.HasIndex(e=>e.RoleId).HasName(Index.RoleId);
            entity.HasIndex(e=>e.UserId).HasName(Index.UserId);

            entity.HasOne(e => e.Role).WithMany(e => e.UserRoles).HasForeignKey(e => e.RoleId).HasConstraintName(Key.RoleIdForeignKey);
            entity.HasOne(e => e.User).WithMany(e => e.UserRoles).HasForeignKey(e => e.UserId).HasConstraintName(Key.UserIdForeignKey);
        }
    }
}