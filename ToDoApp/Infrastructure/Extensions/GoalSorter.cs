using System.Collections.ObjectModel;
using ToDoApp.ViewModels;

namespace ToDoApp.Infrastructure.Extensions
{
    static internal class GoalSorter
    {
        internal static ObservableCollection<GoalViewModel> SortByDate(this ObservableCollection<GoalViewModel> collection)
        {
            var coll = collection.ToList();
            coll.Sort(new GoalComparer());

            collection = new ObservableCollection<GoalViewModel>(coll);

            return collection;
        }
    }

    // top uncompleted goals
    internal class GoalComparer : IComparer<GoalViewModel>
    {
        public int Compare(GoalViewModel? x, GoalViewModel? y)
        {
            if(x.IsCompleted == true && y.IsCompleted == false) return 1;
            if(x.EndDate > y.EndDate && x.EndDate >= DateTime.Now) return -1;
            return 0;
        }
    }
}