using FluentValidation;

namespace ToDo.BL.Validators

{
	public  class CreateToDoValidator : AbstractValidator<CreateToDoDTO>
	{
        public CreateToDoValidator()
        {
            RuleFor(i => i.OwnerId).GreaterThan(0).LessThan(65000).WithMessage("Owner ID Error");
            RuleFor(l => l.Label).Length(3, 100).WithMessage("Error label Length");

        }
    }
}
