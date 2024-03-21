using System.Text.Json;
using ToDoApp.Test;

var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
var s = AppDomain.CurrentDomain.BaseDirectory;

Connector connector = new("SQLITE", s);

var options = new JsonSerializerOptions() { WriteIndented = true };

var result = JsonSerializer.Serialize(connector, options);

File.AppendAllLines(path, [ result ]);

