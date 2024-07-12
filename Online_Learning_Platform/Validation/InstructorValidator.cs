using FluentValidation;
using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.Validation
{
    public class InstructorValidator : AbstractValidator<Instructor>
    {
        public InstructorValidator()
        {
            RuleFor(x => x.InstructorName)
                .NotEmpty()
                .WithMessage("Instructor name is empty");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Instructor mail is empty");

            RuleFor(x => x.MobileNo)
                .NotEmpty()
                .Matches(@"^\d{10}$")
                .WithMessage("Instructor mobile is invalid");

            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
                .WithMessage("Password should be atleast one character, one number, one special character and it length should be atleast 8 and starting can not be number");


            RuleFor(x => x.Expertise)
                .NotEmpty()
                .WithMessage("Instructor expertise is empty");


            RuleFor(x => x.Salary)
                .NotEmpty()
                .WithMessage("Set instructor salary");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("say something in description");
                
        }
    }
}
