using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MigrationPackageSimulator
{
    public class CoffeeMachineStateSaver
    {
        public string _filename = "machine.json";

        public void Save(CoffeeMachineState state)
        {
            var json = JsonConvert.SerializeObject(state);
            File.WriteAllText(_filename, json);
        }

        public CoffeeMachineState Load()
        {
            if (File.Exists(_filename))
            {
                var json = File.ReadAllText(_filename);
                return JsonConvert.DeserializeObject<CoffeeMachineState>(json);
            }

            return new CoffeeMachineState();
        }

        public void ShowStorageJson()
        {
            var json = File.Exists(_filename) ? File.ReadAllText(_filename) : "<empty>";
            Console.WriteLine($"Sored JSON: {json}");
        }
    }
}
