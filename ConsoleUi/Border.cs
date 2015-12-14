using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUi
{
    public class Border : UiElement
    {
        public Border(char horizontal = '-', char vertical = '|', char leftUp = '+', char rightUp = '+', char leftDown = '+', char rightDown = '+')
        {
            LeftUpCorner = leftUp;
            RightUpCorner = rightUp;
            LeftDownCorner = leftDown;
            RightDownCorner = rightDown;
            Horizontal = horizontal;
            Vertical = vertical;
        }
      
        public override void Show()
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.WriteLine(LeftUpCorner + new string(Horizontal, Width+(Offset*2)) + RightUpCorner);
            for (int i = 1; i < Height + (Offset*2); i++)
            {
                Console.SetCursorPosition(Position.X, Position.Y+i);
                Console.WriteLine(Vertical + new string(' ',Width + (Offset*2)) + Vertical);
            }
            Console.SetCursorPosition(Position.X, Position.Y + Height + Offset * 2 );
            Console.WriteLine(LeftDownCorner + new string(Horizontal, Width+ (Offset*2)) + RightDownCorner);
        }



        public int Offset { get; set; } = 1;
        public char LeftUpCorner { get; set; }
        public char RightUpCorner { get; set; }
        public char RightDownCorner { get; set; }
        public char LeftDownCorner { get; set; }
        public char Horizontal { get; set; }
        public char Vertical { get; set; }
        public override int Width { get; set; }
        public override int Height { get; set; }
        public override Point Position { get; set; }
    }
}
