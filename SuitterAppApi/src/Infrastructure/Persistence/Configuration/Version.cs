using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuitterAppApi.Domain.Version;

namespace SuitterAppApi.Infrastructure.Persistence.Configuration;

public class AppVersionConfig : IEntityTypeConfiguration<AppVersion>
{
    public void Configure(EntityTypeBuilder<AppVersion> builder)
    {
        builder.IsMultiTenant();
        builder.ToTable(x => x.IsMemoryOptimized(true));
        builder.Property(b => b.Number).HasMaxLength(256);
        builder.Property(b => b.Note).HasMaxLength(1064);
            
               
    }
}

