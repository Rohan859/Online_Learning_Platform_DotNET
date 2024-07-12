using FluentValidation;
using Online_Learning_Platform.DTOs;

namespace Online_Learning_Platform.Validation
{
    public class UserRegistrationValidator : AbstractValidator<UserRegistrationRequestDTO>
    {
        public UserRegistrationValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email should not be empty");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Please enter password, it is required");


            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Please enter your name");


            RuleFor(x => x.MobileNo)
                .NotEmpty()
                .Matches(@"^\d{10}$")
                .WithMessage("Mobile no is invalid");



        }
    }
}
