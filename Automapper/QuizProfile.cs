using AutoMapper;
using Quizz.DTO;
using Quizz.DTO.AccountDTO;
using Quizz.DTO.QuizDTO;
using Quizz.Entities;

namespace Quizz.Automapper
{
	public class QuizProfile : Profile
	{
		public QuizProfile()
		{
			CreateMap<Quiz, QuizGetDto>();
			CreateMap<Quiz, QuizGetDetailsDto>();
			CreateMap<QuestionPutDto, Question>();
			CreateMap<OptionPutDto, Option>();
			CreateMap<QuizPutDto, Quiz>();
			CreateMap<RegisterDto, AppUser>();
		}
	}
}
