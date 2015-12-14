using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Structure
{
    public class Vacancy
    {
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CompanyCustomer Employer { get; set; }
        public DateTime Date { get; set; }
    }
}
