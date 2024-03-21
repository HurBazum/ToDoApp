using ToDoApp.Interfaces;

namespace ToDoApp.DAL.Entities
{
    public class Goal : IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public bool IsCompleted { get; set; } = false;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}