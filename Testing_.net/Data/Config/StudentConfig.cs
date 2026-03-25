using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Testing_.net.Data.Config
{
    public class StudentConfig: IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(t => t.Id);
            builder. Property(t => t.Id).UseIdentityColumn();

            builder.Property(n => n.Name).IsRequired().HasMaxLength(250);
            builder.Property(n => n.Email).IsRequired().HasMaxLength(250);

            builder.HasData(new List<Student>()
                {
                new Student { Id = 1, Name = "Alice", Email = "a@gmail.com", Age = 20, ERD = new DateTime(2020, 9, 1) },
                new Student { Id = 2, Name = "Bob", Email = "a@gmail.com", Age = 20, ERD = new DateTime(2021, 9, 1) },
                new Student { Id = 3, Name = "Charlie", Email = "a@gmail.com", Age = 20, ERD = new DateTime(2022, 9, 1) }
            });  
        }
    }
}
