using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tema_lab10.Domain;

public class Student: Entity<long>
{
    public Student(long id, string name,string school):base(id)
    {
        Name = name;
        School = school;
    }
    public string Name { get; set; }

    public string School { get; set; }

    public override string ToString()
    {
        return "Student( Id: " + Id + " ; Name: " + Name + " ; School: " + School + " )";
    }
}