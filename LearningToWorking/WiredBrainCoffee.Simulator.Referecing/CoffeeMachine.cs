using System;
using WiredBrainCoffee.Storage.Referencing;

namespace WiredBrainCoffee.Simulator.Referecing
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

    }
}