using System.Windows;

namespace ToDoApp.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для GoalEditorWindow.xaml
    /// </summary>
    public partial class GoalEditorWindow : Window
    {
        public DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text), typeof(string), typeof(GoalEditorWindow), new PropertyMetadata(null));

        public string Text 
        { 
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public DependencyProperty StartDateProperty = DependencyProperty.Register(
            nameof(StartDate), typeof(DateTime), typeof(GoalEditorWindow), new PropertyMetadata(DateTime.Now));

        public DateTime StartDate
        {
            get => (DateTime)GetValue(StartDateProperty);
            set => SetValue(StartDateProperty, value);
        }

        public DependencyProperty EndDateProperty = DependencyProperty.Register(
            nameof(EndDate), typeof(DateTime), typeof(GoalEditorWindow), new PropertyMetadata(DateTime.Now));

        public DateTime EndDate
        {
            get => (DateTime)GetValue(EndDateProperty);
            set => SetValue(EndDateProperty, value);
        }

        public GoalEditorWindow()
        {
            InitializeComponent();
        }
    }
}