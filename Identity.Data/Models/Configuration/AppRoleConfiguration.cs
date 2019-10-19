using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Data.Models.Configuration
{
    using static Identity.Data.Schema.SchemaInfo;
    using static Identity.Data.Schema.SchemaInfo.AppRoleTable;

    internal sealed class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> entity)
        {
            entity.ToTable(TableName, SchemaName);

            entity.HasKey(e => e.Id).HasName(Key.PrimaryKey);

            entity.Property(e => e.ConcurrencyStamp).HasColumnName(Column.ConcurrencyStamp);
            entity.Property(e => e.Id).HasColumnName(Column.Id);
            entity.Property(e => e.Name).HasColumnName(Column.Name);
            entity.Property(e => e.NormalizedName).HasColumnName(Column.NormalizedName);

            entity.HasIndex(e=>e.NormalizedName).HasName(Index.NormalizedName).IsUnique();

            entity.HasMany(e => e.UserRoles).WithOne(e => e.Role).HasForeignKey(e => e.RoleId).HasConstraintName(Key.RoleIdForeignKey);
        }
    }
}