using System;
using WiredBrainCoffee.Simulator.Referecing;

namespace WiredBrainCoffee.ConsoleCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var coffeeMachine = new CoffeeMachine();
            coffeeMachine.MakeCappuccino();
            coffeeMachine.MakeCappuccino();

            Console.WriteLine($"Counter cappuccinno: {coffeeMachine.CounterCappuccino}");
        }
    }
}
