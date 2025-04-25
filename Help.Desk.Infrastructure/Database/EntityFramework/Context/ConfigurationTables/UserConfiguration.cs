using Help.Desk.Infrastructure.Database.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Help.Desk.Infrastructure.Database.EntityFramework.Context.ConfigurationTables;

public class UserConfiguration: IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");
        builder.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd().UseIdentityColumn();
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasColumnName("Name").IsRequired().HasMaxLength(50).IsRequired();
        builder.Property(e => e.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(50).IsRequired();
        builder.Property(e=> e.PhoneNumber).HasColumnName("PhoneNumber").IsRequired().HasMaxLength(10).IsRequired();
        builder.Property(e => e.Email).HasColumnName("Email").IsRequired().HasMaxLength(50).IsRequired();
        builder.Property(e => e.Password).HasColumnName("Password").IsRequired().HasMaxLength(50).IsRequired();
        builder.Property(e => e.DepartmentId).HasColumnName("DepartmentId").IsRequired();
        builder.Property(e => e.Role).HasColumnName("Role").IsRequired().HasMaxLength(20).IsRequired();
        builder.Property(e => e.Active).HasColumnName("Active").IsRequired();
    }
}