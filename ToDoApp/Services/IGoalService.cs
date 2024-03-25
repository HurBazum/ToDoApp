using ToDoApp.ViewModels;

namespace ToDoApp.Services
{
    interface IGoalService
    {
        public string Error { get; set; }
        public List<GoalViewModel> GetAll();
        public Task<object> GetByIdAsync(int id);
        public Task<object> AddGoalAsync(GoalViewModel goalViewModel);
        public Task<object> DeleteGoalAsync(GoalViewModel goalViewModel);
        public Task<object> UpdateGoalAsync(GoalViewModel goalViewModel);
    }
}