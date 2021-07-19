using MigrationPackageSimulator;
using System;

namespace MigrationPackageStorage
{
    public class CoffeeMachine
    {
        private CoffeeMachineStateSaver _coffeeMachineStateSaver;

        public CoffeeMachine()
        {
            _coffeeMachineStateSaver = new CoffeeMachineStateSaver();
            var state = _coffeeMachineStateSaver.Load();
            CounterCappuccino = state.CounterCappuccino;
        }
        public int CounterCappuccino { get; private set; }
        public void MakeCappuccino()
        {
            CounterCappuccino++;
            Console.WriteLine($"Make cappuccinno: {CounterCappuccino}");
            _coffeeMachineStateSaver.Save(new CoffeeMachineState
            {
                CounterCappuccino = CounterCappuccino
            });
        }

        public void ShowStorageJson() { 
            _coffeeMachineStateSaver.ShowStorageJson();
        }
    }
}
