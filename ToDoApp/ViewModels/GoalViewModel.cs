using ToDoApp.ViewModels.Base;

namespace ToDoApp.ViewModels
{
    internal class GoalViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}