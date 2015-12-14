using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Structure
{
    public class Summary
    {
        public Category Category { get; set; }
        public UnEmployed UnEmployed { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UnEmployedName {
            get { return UnEmployed?.Name; }
            set
            {
                if (UnEmployed != null)
                    UnEmployed.Name = value;    
            }
        }
        public DateTime Date { get; set; }
    }


}
