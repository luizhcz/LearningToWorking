using System;
using System.Collections.Generic;
using System.Text;

namespace Demo
{
    public class Employee : People
    {
        public double Salary { get; set; }
        public ProfessionalLevel ProfessionalLevel { get; set; }
        public IList<string> Habilits { get; set; }

        public Employee(string name, double salary)
        {
            Name = string.IsNullOrEmpty(name) ? "Fulano" : name;
            SetSalary(salary);
            SetHabilits();
        }


        private void SetSalary(double salary) {
            if (salary < 500) throw new Exception("Salary less than allowed");

            Salary = salary;
            if (salary < 2000) ProfessionalLevel = ProfessionalLevel.Junior;
            else if (salary >= 2000 && salary < 8000) ProfessionalLevel = ProfessionalLevel.Pleno;
            else if (salary >= 8000) ProfessionalLevel = ProfessionalLevel.Senior;
        }

        private void SetHabilits() {
            var BasicHabilits = new List<string>()
            {
                "Logic of Program",
                "OOP"
            };

            Habilits = BasicHabilits;

            switch (ProfessionalLevel)
            {
                case ProfessionalLevel.Pleno:
                    Habilits.Add("Tests");
                    break;
                case ProfessionalLevel.Senior:
                    Habilits.Add("Tests");
                    Habilits.Add("Microservices");
                    break;
            }

        }
    }

    public class People
    {
        public string Name { get; set; }
        public string NickName { get; set; }
    }

    public enum ProfessionalLevel { 
        Junior,
        Pleno,
        Senior
    }

    public class EmployeeFactory 
    {
        public static Employee Create(string name, double salary) {
            return new Employee(name, salary);
        }
    }
}