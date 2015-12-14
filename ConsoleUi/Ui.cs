using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleUi
{
    public class Ui : UiElement
    {
        public Menu Menu { get; set; }
        public Border Border { get; set; }
        public Pointer Pointer { get; set; }
        public bool CanShow { get; protected set; } = true;

        public Ui(Menu menu, Border border) : this(menu)
        {
            Border = border;
            Pointer.Position = new Point(border.Offset,border.Offset);
            Position = new Point(0,0);
            border.Position = Position;
            RefreshSizes();
        }

        public Ui(Menu menu)
        {
            Menu = menu;
            Menu.Ui = this;
            Pointer = Pointer.Instance;
            Position = new Point(1,0);
            Pointer.PointerMoved += OnPointerMoved;
            
            if (Menu.Items.Count == 0)
            {
                Console.WriteLine("No Items To Show!");
                CanShow = false;
            }
        }

        private void OnPointerMoved(object sender, PointerEventArgs pe)
        {
            Console.Clear();
            Border?.Show();
            Pointer.Show();
            ShowMenuItems();
        }

        public void RefreshSizes()
        {
            Height = Menu.Items.Count;
            var temp = Menu.Items.ToArray();
            Array.Sort(temp, (x, y) => x.Name.Length.CompareTo(y.Name.Length));
            Width = temp.Last().Name.Length;
            Border.Height = Height;
            Border.Width = Width;
        }

        public override void Show()
        {
            Console.Clear();
            if (!CanShow)
            {
                Thread.Sleep(1000);
                return;
                
            }
            RefreshSizes();
            Border?.Show();
            Pointer.Show();
            ShowMenuItems();
            Menu.SelectedItem = Menu.Items[Choice()];
            Menu = Menu.SelectedItem;
            if(Menu == null) return;
            Show();
        }

        public int Choice()
        {
            int CurrentIndex = 0;
            Menu.SelectedItem = Menu.Items[CurrentIndex];
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(Menu.IsNotInteractive).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                    {
                        if (CurrentIndex > 0)
                        {
                            Pointer.MoveUp();
                            CurrentIndex--;
                        }
                    }break;
                    case ConsoleKey.DownArrow:
                    {
                        if (CurrentIndex < Menu.Items.Count-1)
                        {
                            Pointer.MoveDown();
                            CurrentIndex++;
                        }
                    }break;
                }

            } while (key != ConsoleKey.Enter);
            return CurrentIndex;
        }


        private void ShowMenuItems()
        {
            if (Border == null)
            {
                for (int i = 0; i < Height; i++)
                {
                    Console.SetCursorPosition(Position.X, Position.Y + i);
                    Console.WriteLine(Menu.Items[i].Name);
                }
                return;
            }
            for (int i = 0; i < Height; i++)
            {
                Console.SetCursorPosition(Border.Offset+1,Border.Offset+i);
                Console.WriteLine(Menu.Items[i].Name);
            }
        }
        

        public override  int Width { get; set; }
        public override  int Height { get; set; }
        public override  Point Position { get; set; }
    }
}
