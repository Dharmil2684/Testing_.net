using Microsoft.EntityFrameworkCore;

namespace Testing_.net.Data
{
    public class CollegeDBContext : DbContext
    {
        DbSet<Student> Students { get; set; }
         public CollegeDBContext(DbContextOptions<CollegeDBContext> options) : base(options)
        {
        }
    }
}
