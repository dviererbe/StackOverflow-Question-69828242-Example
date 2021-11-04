using System;
using Library.ViewModels;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = DrawerItemList.LoadFromEmmbeddedJsonFile();

            foreach (var item in data.DrawerItems)
            {
                System.Console.WriteLine($"{item.Icon}; {item.Name}");    
            }

            
        }
    }
}
