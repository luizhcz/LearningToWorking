using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Features.Core
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public ValidationResult ValidationResult { get; set; }

        public virtual bool IsValid() {
            throw new NotImplementedException();
        }

    }
}
