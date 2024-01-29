using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quizz.Data;
using Quizz.DTO;
using Quizz.DTO.QuizDTO;
using Quizz.Entities;

namespace Quizz.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuizController : ControllerBase
	{
		private readonly AppDbContext _appDbContext;
		private readonly IMapper _mapper;
		public QuizController(AppDbContext dbContext, IMapper mapper)
		{
			_appDbContext = dbContext;
			_mapper = mapper;
		}

		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var quizzes = _appDbContext.Quizzes.ToList();
			if(quizzes is null) { return NotFound(); }

			var list = new List<QuizGetDto>();

			foreach (var quiz in quizzes)
			{
				var dto = _mapper.Map<Quiz,QuizGetDto>(quiz);
				list.Add(dto);
			}

			return Ok(list);
		}
		[HttpGet("GetById/{id}")]
		[Authorize]
		public IActionResult GetById(int id)
		{
			var quiz = _appDbContext.Quizzes.FirstOrDefault(x => x.Id == id);
			if (quiz is null) return NotFound();

			var questions = _appDbContext.Questiones
				.Include(x => x.Options)
				.Where(x => x.QuizId == id).ToList();

			if (questions is null) return NotFound();

			var optionList = new List<OptionGetDto>();
			
			var list = new List<QuestionGetDto>();


			for(int i = 0; i < questions.Count; i++)
			{
				list.Add(new QuestionGetDto()
				{
					Id = questions[i].Id,
					Name = questions[i].Name,
					Points = questions[i].Points,
					Options = new List<OptionGetDto>
					{
						new OptionGetDto()
						{
							Name = questions[i].Options[i].Name,
							IsCorrect = questions[i].Options[i].IsCorrect
						}
					}
				});

			}

			var dto = new QuizGetDetailsDto()
			{
				Id = id,
				Name = quiz.Name,
				CreationDate = quiz.CreationDate,
				Questions = list
			};

			return Ok(dto);
		}
		[HttpPost("Post")]
		[Authorize(Roles = "Admin")]
		public IActionResult Post([FromBody] QuizPostDTO quizDto)
		{
			var quiz = new Quiz();

			quiz.Name = quizDto.Name;
			quiz.CreationDate = quizDto.CreationDate;
			for(int i = 0; i < quizDto.QuestionPost.Count; i++)
			{
				quiz.Questions.Add(new Question
				{
					Name = quizDto.QuestionPost[i].Name,
					Points = quizDto.QuestionPost[i].Points,
					QuizId = quizDto.Id,

				});
			}

			_appDbContext.Add(quiz);

			_appDbContext.SaveChanges();

			return Ok();
		}
		[HttpPut("Put/{id}")]
		[Authorize(Roles = "Admin")]
		public IActionResult Put(int id, [FromBody] QuizPutDto dto)
		{
			var quiz = _appDbContext.Quizzes.FirstOrDefault(x => x.Id == id);
			if (quiz == null) return NotFound();

			_mapper.Map(dto, quiz);

			_appDbContext.Update(quiz);
			_appDbContext.SaveChanges();

			return Ok();
		}
		[HttpDelete("Delete/{id}")]
		[Authorize(Roles = "Admin")]
		public IActionResult Delete(int id)
		{
			var quiz = _appDbContext.Quizzes.FirstOrDefault(x => x.Id == id);
			if(quiz == null) return NotFound();	

			_appDbContext.Quizzes.Remove(quiz);
			_appDbContext.SaveChanges();

			return Ok();
		}
	}
}
