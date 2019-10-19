using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Data.Models.Configuration
{
    using static Identity.Data.Schema.SchemaInfo;
    using static Identity.Data.Schema.SchemaInfo.AppUserLoginTable;

    internal sealed class AppUserLoginConfiguration : IEntityTypeConfiguration<AppUserLogin>
    {
        public void Configure(EntityTypeBuilder<AppUserLogin> entity)
        {
            entity.ToTable(TableName, SchemaName);
            
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey }).HasName(Key.PrimaryKey);

            entity.Property(e => e.LoginProvider).HasColumnName(Column.LoginProvider);
            entity.Property(e => e.ProviderDisplayName).HasColumnName(Column.ProviderDisplayName);
            entity.Property(e => e.ProviderKey).HasColumnName(Column.ProviderKey);
            entity.Property(e => e.UserId).HasColumnName(Column.UserId);

            entity.HasIndex(e => e.UserId).HasName(Index.UserId);

            entity.HasOne(e => e.User).WithMany(e => e.Logins).HasForeignKey(e => e.UserId).HasConstraintName(Key.UserIdForeignKey);
        }
    }
}