using DapperDemo.Dto;
using FluentValidation;
using FluentValidation.Validators;

#pragma warning disable
namespace DapperDemo.Validator
{
    public class TraineeValidator : AbstractValidator<TraineeDto>
    {
        public TraineeValidator()
        {
            RuleFor(t => t.FirstName).NotEmpty().NotNull().MinimumLength(5)
                .WithMessage("FirstName should not be Null and must exceeds 5 characters");
            RuleFor(t => t.LastName).NotEmpty().NotNull().WithMessage("LastName should not be Null")
                .NotEqual(t => t.FirstName).WithMessage("LastName should not be the same as FirstName");
            RuleFor(t => t.Email).NotEmpty().NotNull().WithMessage("Email should not be Null")
                .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("Please Enter a Valid Email address");
            RuleFor(t => t.City).NotNull().NotEmpty().WithMessage("City should not be Null");
        }
    }
}

