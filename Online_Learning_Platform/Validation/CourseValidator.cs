using FluentValidation;
using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.Validation
{
    public class CourseValidator : AbstractValidator<Course>
    {
        public CourseValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("Course does not exist");


            RuleFor(x => x.CourseName)
                .NotEmpty()
                .WithMessage("course name is empty");

            RuleFor(x => x.CourseDescription)
                .NotEmpty()
                .WithMessage("course description is empty");

            RuleFor(x => x.Category)
                .NotEmpty()
                .WithMessage("category is empty");

            RuleFor(x => x.DifficultyLevel)
                .NotEmpty()
                .WithMessage("difficulty level is empty");

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("course price is not set");

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("Start date can not be empty");

            RuleFor(x => x.EndDate)
               .NotEmpty()
               .WithMessage("End date can not be empty");


        }
    }
}
