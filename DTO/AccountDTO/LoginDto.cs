using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Quizz.DTO.AccountDTO
{
	public class LoginDto
	{
		public string UserName { get; set; }
		public string Password { get; set; }
	}
	public class LoginDtoValidator : AbstractValidator<LoginDto>
	{
        public LoginDtoValidator()
        {
			RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required!");
			RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required!")
									.MinimumLength(6).WithMessage("Minimum length is 6 characters!")
									.MaximumLength(20).WithMessage("Maximum length reached!");
        }
    }
}
