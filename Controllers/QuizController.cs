using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
		public QuizController(AppDbContext dbContext)
		{
			_appDbContext = dbContext;
		}

		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var quizzes = _appDbContext.Quizzes.ToList();
			if(quizzes is null) { return NotFound(); }

			List<QuizGetDto> list = new List<QuizGetDto>();

			foreach (var quiz in quizzes)
			{
				list.Add(new QuizGetDto()
				{
					Name = quiz.Name,
					CreationDate = quiz.CreationDate,
				});
			}

			return Ok(list);
		}
		[HttpGet("GetById/{id}")]
		public IActionResult GetById(int id)
		{
			var quiz = _appDbContext.Quizzes.FirstOrDefault(x => x.Id == id);
			if (quiz is null) return NotFound();

			var questions = _appDbContext.Questiones.Where(x => x.QuizId == id).ToList();
			if (questions is null) return NotFound();

			//var optionList = new List<OptionGetDto>();
			//foreach (var question in questions)
			//{
			//	var options = _appDbContext.Options.Where(x => x.QuestionId == question.Id);
			//	optionList.Add(new OptionGetDto()
			//	{
			//		Id = 
			//	};
			//}

			var list = new List<QuestionGetDto>();

			foreach(var que in questions)
			{
				list.Add(new QuestionGetDto()
				{
					Id = que.Id,
					Name = que.Name,
					Points = que.Points
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
		[HttpPost]
		public IActionResult Post([FromBody] QuizPostDTO dto)
		{
			var quiz = new Quiz();

			quiz.Name = dto.Name;
			quiz.CreationDate = dto.CreationDate;

			_appDbContext.Add(quiz);
			              
			_appDbContext.SaveChanges();

			return Ok();
		}
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] QuizPutDto dto)
		{
			var quiz = _appDbContext.Quizzes.FirstOrDefault(x => x.Id == id);
			if (quiz == null) return NotFound();

			quiz.Name = dto.Name;
			quiz.CreationDate = dto.CreationDate;

			_appDbContext.Update(quiz);
			_appDbContext.SaveChanges();

			return Ok();
		}
	}
}
