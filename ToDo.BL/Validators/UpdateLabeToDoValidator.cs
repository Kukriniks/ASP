using FluentValidation;


namespace ToDo.BL.Validators
{
	public class UpdateLabeToDoValidator : AbstractValidator<UpdateToDoLabelDTO>
	{
        public UpdateLabeToDoValidator()
        {
			RuleFor(l => l.Label).Length(3, 100).WithMessage("Error label Length");
		}
    }
}
