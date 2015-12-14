using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Structure
{
    public class Category
    {
        public Category()
        {
            Summaries = new Collection<Summary>();
            Vacancies = new BindingList<Vacancy>();
        }

        public string Name { get; set; }
        public Collection<Summary> Summaries { get; }
        public Collection<Vacancy> Vacancies { get; }

    }
}
