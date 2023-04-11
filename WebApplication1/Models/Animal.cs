namespace WebApplication1.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int Age { get; set; }     
        public string Type { get; set; } 
        public string Description { get; set; } 
public string? Image { get; set; }
      public State State { get; set; }
        public int StateId { get; set; }
        
    }
}
