using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tema_lab10.Domain;

namespace tema_lab10.Repository;
public interface IRepository<ID, E> where E : Entity<ID>
{
    public IEnumerable<E> FindAll();

    public E GetById(ID id);
}
