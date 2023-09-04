using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EFCore.Configuration;

public class EntityConfiguration
{
    
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Email).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        
        builder.HasIndex(x => x.Email).IsUnique();

    }
}
public class SpentConfiguration : IEntityTypeConfiguration<Spent>
{
    public void Configure(EntityTypeBuilder<Spent> builder)
    {
        builder.Property(x => x.Amount).HasColumnType("float"); 
    }
}
