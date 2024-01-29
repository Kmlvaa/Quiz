using FluentValidation;

namespace Quizz.DTO.AccountDTO
{
	public class RegisterDto
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
	public class RegisterDtoValidator : AbstractValidator<RegisterDto>
	{
        public RegisterDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name required!");
			RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name required!");
			RuleFor(x => x.Username).NotEmpty().WithMessage("User name required!");
			RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required!")
								 .EmailAddress().WithMessage("Enter valid email address!");
			RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required!")
									.MinimumLength(6).WithMessage("Minimum length is 6 characters!")
									.MaximumLength(20).WithMessage("Maximum length reached!");
		}
	}
}
