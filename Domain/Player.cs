using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tema_lab10.Domain;

public class Player : Student
{
    public Player(long id, string name,string school,Team team): base(id, name, school)
    {
        Team = team;
    }

    public Team Team { get; set; }

    public override string ToString()
    {
        return base.ToString() + " Player( "+ Team + " )";
    }
}