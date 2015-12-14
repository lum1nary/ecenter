using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUi
{
    public  abstract class UiElement
    {
        public abstract void Show();
        public abstract int Width { get; set; }
        public abstract int Height { get; set; }
        public abstract Point Position { get; set; }
    }
}
