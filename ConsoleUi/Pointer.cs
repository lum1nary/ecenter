using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUi
{
    public class Pointer : UiElement
    {
        private Point _position;
        public override void Show()
        {
            Console.SetCursorPosition(_position.X, _position.Y);
            Console.Write(Cursor);
        }

        public override int Width { get { return 1; } set {return;} } 
        public override int Height { get { return 1; } set {return;} }
        public override Point Position { get { return _position ?? (_position = new Point(0, 0)); } set { _position = value; } }

        public char Cursor { get; set; }

        public delegate void PointerActionsDelegate(object sender, PointerEventArgs pe);

        public static event PointerActionsDelegate PointerMoved;

        protected static Pointer _instance;

        public static Pointer Instance => _instance ?? new Pointer(new Point(0, 0),'>');
        protected Pointer(Point startPos, char cursor)
        {
            Position = startPos;
            Cursor = cursor;
        }

        static Pointer()
        {
            Console.CursorVisible = false;
        }
        public void MoveUp(int lines = 1)
        {
            Point _old = Position;
            Position.Up(lines);
            Point _new = Position;
            PointerMoved?.Invoke(this,new PointerEventArgs(_old,_new));
        }
        public void MoveDown(int lines = 1)
        {
            Point _old = Position;
            Position.Down(lines);
            Point _new = Position;
            PointerMoved?.Invoke(this, new PointerEventArgs(_old, _new));
        }
    }

    #region Pointer EventArgs
    public class PointerEventArgs : EventArgs
    {
        public PointerEventArgs(Point oldPosition,  Point newPosition)
        {
            OldPosition = oldPosition;
            NewPosition = newPosition;
        }
        public Point OldPosition { get;}
        public Point NewPosition { get;}
    }
    #endregion
}
