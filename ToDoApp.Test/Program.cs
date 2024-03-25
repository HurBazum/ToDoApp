using System.Text.Json;
using ToDoApp.Test;


DateTime dateTime = DateTime.Now;

Console.WriteLine(dateTime);

Console.WriteLine("________________");

string dateOnly = dateTime.ToShortDateString();
var timeOnly = dateTime.TimeOfDay;

Console.WriteLine(dateOnly);
Console.WriteLine(timeOnly);
Thread.Sleep(2000);
DateTime dateTime1 = DateTime.Parse(string.Concat(dateOnly, ", ", timeOnly));

Console.WriteLine(dateTime1);