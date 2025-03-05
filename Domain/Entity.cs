using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tema_lab10.Domain;
public class Entity<ID>
{
    public Entity(ID id)
    {
        Id= id;
    }
    public ID Id { get; set; }
}
