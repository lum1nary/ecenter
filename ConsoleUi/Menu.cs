
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ConsoleUi;

namespace ConsoleUi
{
    public class Menu
    {
        public static object Data; 
        public Ui Ui { get; set; }

        public Collection<Menu> Items = new Collection<Menu>();
        public string Name { get; }
        private Menu _selectedItem;

        public Action<Menu> Action;
        public Menu SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                _selectedItem.Action.Invoke(this);
            }
        }

        public bool IsNotInteractive { get; set; } = true;

        public Menu Parent;
        
        public Menu(string menu ,Action<Menu> ActionOnSelected ,params Menu[] items)
        {
            Name = menu;
            foreach (var item in items)
            {
                item.Parent = this;
                Items.Add(item);
            }
            Action = ActionOnSelected;
        }

        //public MenuItem this[string itemName]
        //{
        //    get
        //    {
        //        try
        //        {
        //            return Items.First(item => item.Name == itemName);
        //        }
        //        catch (NullReferenceException)
        //        {
        //            return null;
        //        }
        //    }
        //}
        

    }

    


    //public class MenuItem
    //{
    //    public MenuItem(string name,Action action, Menu connectedMenu = null, Menu menu = null)
    //    {
    //        Name = name;            
    //        Menu = menu;
    //        ConnectedMenu = connectedMenu;
    //        Action = action;
    //    }

    //    public Action Action { get; }

    //    public Menu Menu { get; set; }
    //    public string Name { get; }
    //    public int ItemIndex { get; set; }
    //    public Menu ConnectedMenu { get; }
    //}
}
