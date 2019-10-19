using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Data.Models.Configuration
{
    using static Identity.Data.Schema.SchemaInfo;
    using static Identity.Data.Schema.SchemaInfo.AppUserClaimTable;

    internal sealed class AppUserClaimConfiguration : IEntityTypeConfiguration<AppUserClaim>
    {
        public void Configure(EntityTypeBuilder<AppUserClaim> entity)
        {
            entity.ToTable(TableName, SchemaName);

            entity.HasKey(e => e.Id).HasName(Key.PrimaryKey);

            entity.Property(e => e.ClaimType).HasColumnName(Column.ClaimType);
            entity.Property(e => e.ClaimValue).HasColumnName(Column.ClaimValue);
            entity.Property(e => e.Id).HasColumnName(Column.Id);
            entity.Property(e => e.UserId).HasColumnName(Column.UserId);

            entity.HasIndex(e => e.UserId).HasName(Index.UserId);

            entity.HasOne(e => e.User).WithMany(e => e.Claims).HasForeignKey(e => e.UserId).HasConstraintName(Key.UserIdForeignKey);
        }
    }
}