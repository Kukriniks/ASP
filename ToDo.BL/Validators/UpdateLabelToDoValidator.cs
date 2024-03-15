using FluentValidation;

namespace ToDo.BL.Validators
{
	public class UpdateLabelToDoValidator : AbstractValidator<UpdateToDoLabelDTO>
	{
        public UpdateLabelToDoValidator()
        {
			RuleFor(l => l.Label).Length(3, 100).WithMessage("Error label Length");
		}
    }
}
