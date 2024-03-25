using FluentValidation;
using Users.BL.UserDTO;

namespace Users.BL.Validators
{
	public class CreateUserValidator : AbstractValidator<CreateUserDTO>
	{
		public CreateUserValidator()
		{
			RuleFor(i => i.Name).NotEmpty().MinimumLength(3).MaximumLength(200).NotNull().WithMessage("UserName Error");
			RuleFor(i => i.Login).NotEmpty().MinimumLength(3).MaximumLength(200).NotNull().WithMessage("UserLogin Error");
			RuleFor(i => i.Password).NotEmpty().MinimumLength(6).MaximumLength(15).NotNull().WithMessage("UserPass Error");
		}
	}
}

