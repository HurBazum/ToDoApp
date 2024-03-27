namespace ToDoApp.Test
{
    internal class Something
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string Description => $"{Id}: {Name}";
    }
}