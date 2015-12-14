using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Structure
{
    public class CompanyCustomer
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Collection<Vacancy> Vacancies { get; set; }
        public string RegisterNumber { get; set; }
    }
}
