using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;
using ToDoApp.Infrastructure.Commands;
using ToDoApp.Services;
using ToDoApp.ViewModels.Base;

namespace ToDoApp.ViewModels
{
    internal class GoalViewModel : BaseViewModel
    {
        private string text = null!;
        private DateTime start;
        private DateTime end = default;
        private bool complited;
        private string actualState = string.Empty;
        private string goalTimer = string.Empty;
        public int Id { get; set; }
        public string Text
        {
            get => text;
            set => Set(ref text, value);
        }
        public DateTime StartDate
        {
            get => start;
            set
            {
                Set(ref start, value);
                if(EndDate != default)
                {
                    GoalTimer = GetActualTime();
                    ActualState = GetActualState();
                }
            }
        }
        public DateTime EndDate
        {
            get => end;
            set
            {
                Set(ref end, value);
                GoalTimer = GetActualTime();
                ActualState = GetActualState();
            }
        }
        public bool IsCompleted
        {
            get => complited;
            set => Set(ref complited, value);
        }

        public string GoalTimer
        {
            get => goalTimer;
            set => Set(ref goalTimer, value);
        }
        public string ActualState 
        { 
            get => actualState;
            set => Set(ref actualState, value);
        }

        #region Commands

        #region Change status

        private ICommand? completeGoalCommand;
        public ICommand CompleteGoalCommand => completeGoalCommand ??= new LambdaCommand(OnCompleteGoalCommand, CanCompleteGoalCommand);

        public bool CanCompleteGoalCommand(object parameter)
        {
            if(IsCompleted == true)
            {
                return false;
            }

            return true;
        }

        public async void OnCompleteGoalCommand(object parameter)
        {
            IsCompleted = !IsCompleted;

            var goalService = App.Host.Services.GetRequiredService<IGoalService>();
            var result = await goalService.UpdateStatusGoalAsync(this);
            if(result is string)
            {
                MessageBox.Show(result as string);
            }
        }

        #endregion

        #endregion


        #region Property changers
        string GetActualState()
        {
            int daysToEnd = (EndDate - DateTime.Now).Days;
            if(daysToEnd < 2 && daysToEnd > 0 && IsCompleted == false)
            {
                return "#FFF1E69B";
            }
            if(daysToEnd < 0 && IsCompleted == false)
            {
                return "#FFF2B8C1";
            }
            else
            {
                return "#FFA6D2BB";
            }
        }

        string GetActualTime()
        {
            return $"С {StartDate} до {EndDate}";
        }

        #endregion
    }
}