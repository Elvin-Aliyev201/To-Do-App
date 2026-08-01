using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Api.Entities;

namespace ToDo.Api.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.Username)
                   .IsUnique();

            builder.Property(u => u.PasswordHash)
                   .IsRequired();
        }
    }
}
