using FluentValidation;
using System;

namespace TainaTech.Application.Features.Persons.Commands.UpdatePerson
{
    public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator()
        {
            RuleFor(p => p.Firstname)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");

            RuleFor(p => p.Surname)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 30 characters.");

            RuleFor(p => p.Gender)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.EmailAddress)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(254).WithMessage("{PropertyName} must not exceed 30 characters.");

            RuleFor(p => p.PhoneNumber)
                .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");

            RuleFor(p => p.DateOfBirth)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .LessThan(DateTime.Now);
        }
    }

}
