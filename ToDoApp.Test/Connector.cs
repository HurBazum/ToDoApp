using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Test
{
    internal class Connector
    {
        public string Type { get; set; } = null!;
        public Dictionary<string, string> Parameters { get; set; } = new();

        public Connector(string s0, string s1)
        { 
            Type = s0;
            Parameters.Add(s0, s1);
        }


        public void AddParameters(string s)
        {
            Parameters[Type] = s;
        }
    }
}
