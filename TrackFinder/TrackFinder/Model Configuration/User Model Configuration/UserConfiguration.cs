using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackFinder.Models.UserModels;

namespace TrackFinder.Infrastructure.Model_Validation.User_Model_Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable("Users");

		builder.HasKey(u => u.Id);

		builder.Property(u => u.Id)
			  .ValueGeneratedOnAdd();

		builder.HasIndex(u => u.Username).IsUnique();

		builder.HasIndex(u => u.Email).IsUnique();

		builder.Property(u => u.Username)
			  .IsRequired()
			  .HasMaxLength(100);

		builder.Property(u => u.FirstName)
			  .IsRequired()
			  .HasMaxLength(100);

		builder.Property(u => u.LastName)
			  .IsRequired()
			  .HasMaxLength(100);

		builder.Property(u => u.Email)
			  .IsRequired()
			  .HasMaxLength(256);

		builder.Property(u => u.PasswordHash)
			  .IsRequired();

		builder.Property(u => u.Gender)
			  .HasMaxLength(20);

		builder.Property(u => u.Bio)
			  .HasMaxLength(500);

		builder.Property(u => u.ProfilePictureUrl)
			  .HasMaxLength(500);

		builder.Property(u => u.Role)
			  .IsRequired()
			  .HasMaxLength(20);

		builder.Property(u => u.CreatedAt)
			  .HasDefaultValueSql("GETUTCDATE()");
	}
}
