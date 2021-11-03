using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClient.Models.Persons.Interfaces
{
    public interface IFullName
    {
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}
