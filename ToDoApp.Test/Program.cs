using ToDoApp.Test;

namespace ToDoApp.Test
{
    public class A
    {
        public bool BoolProperty { get; set; }
        public DateTime DateTimeProperty { get; set; }

        public override string ToString()
        {
            return $"boolProperty:{BoolProperty} - dateTimeProperty:{DateTimeProperty.ToString("dd.MM.yyyy")}";
        }
    }

    public class AComparer : IComparer<A>
    {
        public int Compare(A? x, A? y)
        {
            if(x.BoolProperty == true && y.BoolProperty == false) return 1;
            if(x.DateTimeProperty > y.DateTimeProperty && x.DateTimeProperty >= DateTime.Now) return -1;
            return 0;
        }
    }

    public class IntComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if(x > y) return 1;
            if(x < y) return -1;
            return 0; 
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //List<int> list = [9, 8, 7, 6, 5, 4];

            //list.Sort(new IntComparer());

            //ShowList(list);

            A a = new() { BoolProperty = true, DateTimeProperty = DateTime.Now };
            A b = new() { BoolProperty = false, DateTimeProperty = DateTime.Now };
            A c = new() { BoolProperty = false, DateTimeProperty = DateTime.Now.AddDays(1) };

            List<A> listA = [a, b, c];

            ShowList(listA);

            listA.Sort(new AComparer());

            ShowList(listA);
        }

        static void ShowList<T>(List<T> list)
        {
            foreach(var item in list)
            {
                if(item is not null)
                {
                    Console.WriteLine(item.ToString());
                }
                else
                {
                    Console.WriteLine("there is nullable element");
                }
            }

            Console.WriteLine("\n_______________________\n");
        }
    }
}