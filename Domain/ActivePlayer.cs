using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tema_lab10.Domain;

public class ActivePlayer
{
    public ActivePlayer(long idPlayer, long idComp,long points,string type)
    {
        IdPlayer = idPlayer;
        IdComp = idComp;
        Points = points;
        Type = type;
    }
    public long IdPlayer { get; set; }
    public long IdComp { get; set; }
    public long Points { get; set; }
    public string Type { get; set; }
}