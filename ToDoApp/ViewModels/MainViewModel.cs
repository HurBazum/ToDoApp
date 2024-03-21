using ToDoApp.Services;
using ToDoApp.ViewModels.Base;
using System.Windows.Input;
using ToDoApp.Infrastructure.Commands;
using System.Collections.ObjectModel;

namespace ToDoApp.ViewModels
{
    class MainViewModel(IGoalService goalService) : BaseViewModel
    {
        private readonly IGoalService _goalService = goalService;

        #region model's property

        public ObservableCollection<GoalViewModel> Goals => new(_goalService.GetAll());

        private string _title = "ToDoApp"; 
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private int _count = 0;
        public int Count
        {
            get => _count;
            set => Set(ref _count, value);
        }

        private string _error = string.Empty;
        public string Error
        {
            get => _error;
            set => Set(ref _error, value);
        }

        private DateTime _start = DateTime.Now;
        public DateTime Start
        {
            get => _start;
            set
            {
                if(value >= DateTime.Now)
                {
                    _start = value;
                    OnPropertyChanged(nameof(Start));
                    End = value;
                }
            }
        }

        private DateTime _end = DateTime.Now;
        public DateTime End
        {
            get => _end;
            set
            {
                if(value >= _start)
                {
                    _end = value;
                    OnPropertyChanged(nameof(End));
                }
            }
        }

        private DateTime _searchDate = DateTime.Now;
        public DateTime SearchDate
        {
            get => _searchDate;
            set => Set(ref _searchDate, value);
        }

        private string _goalText = string.Empty;
        public string GoalText
        {
            get => _goalText;
            set => Set(ref _goalText, value);
        }

        private GoalViewModel? _selectedGoal;
        public GoalViewModel? SelectedGoal
        {
            get => _selectedGoal;
            set => Set(ref _selectedGoal, value);
        }

        #endregion

        #region Commands

        #region Add a goal

        private ICommand? addGoalCommand;
        public ICommand AddGoalCommand => addGoalCommand ??= new LambdaCommand(OnAddGoalCommand, CanAddGoalCommand);

        public bool CanAddGoalCommand(object parameter)
        {
            if(GoalText == string.Empty)
            {
                return false;
            }

            return true;
        }
        public async void OnAddGoalCommand(object parameter)
        {
            GoalViewModel goalViewModel = new()
            {
                StartDate = Start,
                EndDate = End,
                Text = GoalText,
                IsCompleted = false
            };

            var result = await _goalService.AddGoalAsync(goalViewModel);

            if(result is string)
            {
                Error = result.ToString();
            }
            else
            {
                Goals.Add((GoalViewModel)result);
                OnPropertyChanged(nameof(Goals));
                GoalText = string.Empty;
                Error = "Цель успешно добавлена";
            }
        }

        #endregion

        #region Delete a Goal

        private ICommand? deleteGoalCommand;
        public ICommand DeleteGoalCommand => deleteGoalCommand ??= new LambdaCommand(OnDeleteGoalCommand, CanDeleteGoalCommand);
        public bool CanDeleteGoalCommand(object parameter)
        {
            if(SelectedGoal is null)
            {
                return false;
            }

            return true;
        }

        public async void OnDeleteGoalCommand(object parameter)
        {
            var result = await _goalService.DeleteGoalAsync(SelectedGoal);

            if(result is string)
            {
                Error = result.ToString();
            }
            else
            {
                Goals.Remove(SelectedGoal);
                OnPropertyChanged(nameof(Goals));
                SelectedGoal = null;
                Error = "Цель успешно удалена";
            }
        }

        #endregion

        #region update a goal

        private ICommand? updateGoalCommand;
        public ICommand UpdateGoalCommand => updateGoalCommand ??= new LambdaCommand(OnUpdateGoalCommand, CanUpdateGoalCommand);

        public bool CanUpdateGoalCommand(object parameter)
        {
            if(SelectedGoal is null)
            {
                return false;
            }

            return true;
        }

        public void OnUpdateGoalCommand(object parameter)
        {
            SelectedGoal = null;
        }

        #endregion

        #endregion
    }
}