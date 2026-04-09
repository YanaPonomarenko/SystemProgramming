using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankQueueSimulation.Models;

public class Client
{
    public string Name { get; }
    public string VisitPurpose { get; }

    public Client(string name, string visitPurpose)
    {
        Name = name;
        VisitPurpose = visitPurpose;
    }

    public override string ToString()
    {
        return $"{Name} (мета: {VisitPurpose})";
    }
}
