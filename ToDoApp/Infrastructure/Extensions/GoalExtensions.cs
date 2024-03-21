using ToDoApp.DAL.Entities;
using ToDoApp.ViewModels;

namespace ToDoApp.Infrastructure.Extensions
{
    internal static class GoalExtensions
    {
        public static List<GoalViewModel> GoalToView(ICollection<Goal> goals)
        {
            var vms = goals.Select(item => new GoalViewModel 
            { 
                Id = item.Id, 
                Text = item.Text, 
                StartDate = item.StartDate, 
                EndDate = item.EndDate, 
                IsCompleted = item.IsCompleted
            }).ToList();

            return vms;
        }

        public static GoalViewModel GoalToView(Goal goal)
        {
            var vm = new GoalViewModel
            {
                Id = goal.Id,
                Text = goal.Text,
                StartDate = goal.StartDate,
                EndDate = goal.EndDate,
                IsCompleted = goal.IsCompleted
            };

            return vm;
        }

        public static Goal ViewToGoal(GoalViewModel vm)
        {
            Goal goal = new()
            {
                Id = vm.Id,
                Text = vm.Text,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                IsCompleted = vm.IsCompleted
            };

            return goal;
        }
    }
}