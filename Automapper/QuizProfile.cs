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
			CreateMap<QuestionPutDto, Question>();
			CreateMap<QuizPutDto, Quiz>();
			CreateMap<RegisterDto, AppUser>();

			CreateMap<Quiz, QuizGetDetailsDto>()
				.ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
				.ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));
			CreateMap<Question, QuestionGetDto>()
				.ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options));
			CreateMap<Option, OptionGetDto>();

			CreateMap<QuizPostDto, Quiz>()
				.ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
				.ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.QuestionPost));
			CreateMap<QuestionPostDto, Question>()
				.ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.OptionPost));
			CreateMap<OptionPostDto, Option>();
		}
	}
}
