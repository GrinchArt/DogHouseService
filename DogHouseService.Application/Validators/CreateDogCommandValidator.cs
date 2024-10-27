using DogHouseService.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogHouseService.Application.Validators
{
    public class CreateDogCommandValidator : AbstractValidator<CreateDogCommand>
    {
        public CreateDogCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

            RuleFor(x => x.Color)
                .NotEmpty().WithMessage("Color is required.")
                .MaximumLength(30).WithMessage("Color cannot exceed 30 characters.");

            RuleFor(x => x.TailLength)
                .GreaterThanOrEqualTo(0).WithMessage("Tail length must be a non-negative number.");

            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Weight must be a positive number.");
        }
    }
}
