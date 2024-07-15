
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfigurations: IEntityTypeConfiguration<User>{
     public void Configure(EntityTypeBuilder<User> builder){
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedNever();
        builder.Property(u => u.Name).IsRequired();
        builder.Property(u => u.Email).IsRequired().HasMaxLength(20);
     }
}