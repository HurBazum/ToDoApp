using Microsoft.Extensions.DependencyInjection;
using ToDoApp.DAL.Entities;
using ToDoApp.Infrastructure.Extensions;
using ToDoApp.Interfaces;
using ToDoApp.ViewModels;

namespace ToDoApp.Services
{
    class GoalService : IGoalService
    {
        private readonly IRepository<Goal> _goalRepository;
        public string Error { get; set; } = string.Empty;
        public GoalService() 
        {
            _goalRepository = App.Host.Services.GetRequiredService<IRepository<Goal>>();
        }

        public List<GoalViewModel> GetAll() => GoalExtensions.GoalToView([.. _goalRepository.GetAll().Result]);

        public async Task<object> GetByIdAsync(int id)
        {
            object result;

            try
            {
                var goal = await _goalRepository.GetItemById(id);
                result = GoalExtensions.GoalToView(goal);
            }
            catch(Exception ex)
            {
                result = ex.Message ?? "Необработанная ошибка";
            }

            return result;
        }

        public async Task<object> AddGoalAsync(GoalViewModel goalViewModel)
        {
            object result;

            try
            {
                var exists = GetAll().FirstOrDefault(e => e.Text == goalViewModel.Text);

                if(exists is not null)
                {
                    throw new Exception("Такая цель уже существует");
                }
                else
                {
                    var goal = GoalExtensions.ViewToGoal(goalViewModel);
                    result = GoalExtensions.GoalToView(await _goalRepository.AddItem(goal));
                }
            }
            catch(Exception ex)
            {
                result = ex.Message ?? "Необработанная ошибка";
            }

            return result;
        }

        public async Task<object> DeleteGoalAsync(GoalViewModel goalViewModel)
        {
            object result;

            try
            {
                result = await _goalRepository.DeleteItem(goalViewModel.Id);
            }
            catch(Exception ex)
            {
                result = ex.Message ?? "Необработанная ошибка";
            }

            return result;
        }

        public async Task<object> UpdateGoalAsync(GoalViewModel goalViewModel)
        {
            object result;

            try
            {
                var goal = await _goalRepository.GetItemById(goalViewModel.Id);

                goal.Text = goalViewModel.Text;
                goal.StartDate = goalViewModel.StartDate;
                goal.EndDate = goalViewModel.EndDate;

                result = GoalExtensions.GoalToView(await _goalRepository.UpdateItem(goal));
            }
            catch(Exception ex)
            {
                result = ex.Message ?? "Необработанная ошибка";
            }

            return result;
        }

        public async Task<object> UpdateStatusGoalAsync(GoalViewModel goalViewModel)
        {
            object result;

            try
            {
                var goal = await _goalRepository.GetItemById(goalViewModel.Id);

                goal.IsCompleted = goalViewModel.IsCompleted;

                result = GoalExtensions.GoalToView(await _goalRepository.UpdateItem(goal));
            }
            catch(Exception ex)
            {
                result = ex.Message ?? "Необработанная ошибка";
            }

            return result;
        }
    }
}