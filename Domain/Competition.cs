using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tema_lab10.Domain;
    
public class Competition:Entity<long>
{
    public Competition(long id,Team team1,  Team team2, DateOnly date):base(id)
    {
        Team1=team1;
        Team2=team2;
        Date=date;
    }

    public Team Team1 { get; set; }
    public Team Team2 { get; set; }
    public DateOnly Date {  get; set; }

    public override string ToString()
    {
        return "Competiton ( ID: " + Id +" "+ Team1.ToString() + " " + Team2.ToString() + " " + "Date ( " + Date.ToString() + " )";
    }

}
