using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tema_lab10.Domain;
public class Team: Entity<long>
{
    public Team(long id, string name):base(id)
    {
        Name = name;
    }

    public string Name { get; set; }

    public override string ToString()
    {
        return "Team( ID: " + Id + " ; Name: "+ Name+" )";
    }
}