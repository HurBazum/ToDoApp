using ToDoApp.Services;
using ToDoApp.ViewModels.Base;
using System.Windows.Input;
using ToDoApp.Infrastructure.Commands;
using System.Collections.ObjectModel;

namespace ToDoApp.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private readonly IGoalService _goalService;

        #region model's property

        public ObservableCollection<GoalViewModel> Goals { get; set; } 

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
                if(value >= DateTime.Now && IsUpdated == "Collapsed")
                {
                    _start = value;
                    OnPropertyChanged(nameof(Start));
                    End = value;
                }
                else
                {
                    _start = value;
                    OnPropertyChanged(nameof(Start));
                }
            }
        }

        private DateTime _end = DateTime.Now;
        public DateTime End
        {
            get => _end;
            set
            {
                if(value >= _start && IsUpdated == "Collapsed")
                {
                    _end = value;
                    OnPropertyChanged(nameof(End));
                }
                else
                {
                    _end = value;
                    OnPropertyChanged(nameof(End));
                }
            }
        }


        #region Other dates

        #region date for serching

        private DateTime _searchDate = DateTime.Now;
        public DateTime SearchDate
        {
            get => _searchDate;
            set => Set(ref _searchDate, value);
        }

        #endregion

        #endregion

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

        private string isUpdated = "Collapsed";
        public string IsUpdated
        {
            get => isUpdated;
            set => Set(ref isUpdated, value);
        }

        private int selectedIndex;
        public int SelectedIndex
        {
            get => selectedIndex;
            set => Set(ref selectedIndex, value);
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
            IsUpdated = "Visible";

            Start = SelectedGoal.StartDate;
            End = SelectedGoal.EndDate;

            GoalText = SelectedGoal.Text;
            SelectedIndex = SelectedGoal.Id;
        }

        #region help commands

        #region cancel update

        private ICommand? cancelUpdateGoalCommand;
        public ICommand CancelUpdateGoalCommand => cancelUpdateGoalCommand ??= new LambdaCommand(OnCancelUpdateGoalCommand, CanCancelUpdateGoalCommand);

        public bool CanCancelUpdateGoalCommand(object parameter) => true;
        public async void OnCancelUpdateGoalCommand(object parameter)
        {
            IsUpdated = "Collapsed";
            Start = DateTime.Now;
            End = DateTime.Now;
            GoalText = string.Empty;
        }

        #endregion

        #region accept update a goal

        private ICommand? acceptUpdateGoalCommand;
        public ICommand AcceptUpdateGoalCommand => acceptUpdateGoalCommand ??= new LambdaCommand(OnAcceptUpdateGoalCommand, CanAcceptUpdateGoalCommand);

        public bool CanAcceptUpdateGoalCommand(object parameter)
        {
            if(SelectedGoal is null)
            {
                return false;
            }
            else
            {
                if(!Equals(Start, SelectedGoal.StartDate) || !Equals(End, SelectedGoal.EndDate) || !Equals(GoalText, SelectedGoal.Text))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async void OnAcceptUpdateGoalCommand(object parameter)
        {
            var goal = (GoalViewModel)(await _goalService.GetByIdAsync(SelectedIndex));

            if(!Equals(Start, goal.StartDate))
            {
                goal.StartDate = Start;
            }
            if(!Equals(End, goal.EndDate))
            {
                goal.EndDate = End;
            }
            if(!Equals(GoalText, goal.Text))
            {
                goal.Text = GoalText;
            }
            
            var result = await _goalService.UpdateGoalAsync(goal);

            if(result is string)
            {
                Error = result.ToString();
            }
            else
            {
                IsUpdated = "Collapsed";
                Start = DateTime.Now;
                End = DateTime.Now;
                GoalText = string.Empty;
                var gvm = await _goalService.GetByIdAsync(SelectedIndex) as GoalViewModel;
                int index = Goals.IndexOf(Goals.FirstOrDefault(vm => vm.Id == SelectedIndex));
                Goals[index] = gvm;
                OnPropertyChanged(nameof(Goals));
            }
        }

        #endregion

        #endregion

        #endregion

        #endregion

        public MainViewModel(IGoalService goalService)
        {
            _goalService = goalService;
            Goals = new(_goalService.GetAll());
        }
    }
}