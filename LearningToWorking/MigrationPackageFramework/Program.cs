using MigrationPackageStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationPackageFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            var coffeeMachine = new CoffeeMachine();
            coffeeMachine.MakeCappuccino();
            coffeeMachine.MakeCappuccino();

            Console.WriteLine($"Counter cappuccinno: {coffeeMachine.CounterCappuccino}");
            coffeeMachine.ShowStorageJson();
            Console.ReadLine();
        }
    }
}