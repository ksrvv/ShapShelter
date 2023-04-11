namespace WebApplication1.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string Name { get; set; }  // имя сотрудника
        public int Age { get; set; } // возраст сотрудника         
        public string Position { get; set; } // дожность сотрудника
        public string Quote { get; set; } // цитата сотрудника
        public string? Image { get; set; }

    }
}
