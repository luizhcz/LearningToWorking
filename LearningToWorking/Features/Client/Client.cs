using Features.Core;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Features.Client
{
    public class Client : Entity
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public DateTime NascDate { get; set; }
        public DateTime CadDate { get; set; }
        public string Mail { get; set; }
        public bool Active { get; set; }

        protected Client()
        {

        }

        public Client(Guid id, string name, string lastName, DateTime nascDate, DateTime cadDate, string mail, bool active)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            NascDate = nascDate;
            CadDate = cadDate;
            Mail = mail;
            Active = active;
        }

        public string CompleteName() {
            return $"{LastName} {Name}";        
        }

        public bool IsSpecial() {
            return CadDate > DateTime.Now.AddYears(-3) && Active;
        }

        public void Inative() {
            Active = false;
        }

        public override bool IsValid() {
            ValidationResult = new ClientValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }

    public class ClientValidator : AbstractValidator<Client> 
    {
        public ClientValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name not inserted")
                .Length(2, 150).WithMessage("Name must have between 2 and 150 caracteres ");
                
 
            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("LastName not inserted")
                .Length(2, 150).WithMessage("LastName must have between 2 and 150 caracteres ");

            RuleFor(c => c.NascDate)
                .NotEmpty()
                .Must(HaveMinimumAge)
                .WithMessage("The Client Must have 18 years or more");

            RuleFor(c => c.Mail)
                .NotEmpty()
                .EmailAddress();

            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        public static bool HaveMinimumAge(DateTime birthdate) {
            return birthdate <= DateTime.Now.AddYears(-18);
        }
    }
}
