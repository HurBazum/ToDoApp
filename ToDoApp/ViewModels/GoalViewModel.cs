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
        private DateTime end;
        private bool complited;
        private string actualState = string.Empty;
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
            }
        }
        public DateTime EndDate
        {
            get => end;
            set
            {
                Set(ref end, value);
            }
        }
        public bool IsCompleted
        {
            get => complited;
            set
            {
                Set(ref complited, value);
                if(value == false && EndDate < DateTime.Now)
                {
                    ActualState = "#FFF2B8C1";
                }
                if(value == false && (DateTime.Now < EndDate && EndDate <= DateTime.Now.AddDays(2))) 
                { 
                    ActualState = "#FFF1E69B";
                }
                if(value == true || EndDate > DateTime.Now)
                {
                    ActualState = "#FF7BB1B1";
                }
            }
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
            if(IsCompleted == true || EndDate < DateTime.Now)
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


        #region Colors

        // ??

        /*
         * "#FFF1E69B"; // yellow
         * "#FFF2B8C1"; // red
         * "#FFA6D2BB"; // green
         * "#FF7BB1B1"; // azure dark
         * */

        #endregion
    }
}