using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStack.Models;

public class User
{
    public int Id { get; set; }
    public string ActionName { get; set; }
    public DateTime Timestamp { get; set; }
    public string UserName { get; set; } 

    public User(int id, string actionName, string userName = "Користувач")
    {
        Id = id;
        ActionName = actionName;
        Timestamp = DateTime.Now;
        UserName = userName; 
    }

    public override string ToString()
    {
        return $"[{Timestamp:HH:mm:ss.fff}] {UserName}: {ActionName}";
    }
}
