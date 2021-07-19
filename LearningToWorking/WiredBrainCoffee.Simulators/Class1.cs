using System;

namespace WiredBrainCoffee.Simulators
{
    public class CoffeeMachine
    {
        public int CounterCappuccino { get; private set; }
        public void MakeCappuccino() {
            CounterCappuccino++;
        }

    }
}
