namespace Testing_.net.Models
{
    public static class SchoolRepo
    {
        public static List<Student> Students { get => field; set => field = value; } = new List<Student>
        {
            new Student { Id = 1, Name = "Alice", Age = 20, Email = "abc@gmail.com" },
            new Student { Id = 2, Name = "Bob", Age = 22, Email = "xyz@gmail.com" }
        };
    }
}
