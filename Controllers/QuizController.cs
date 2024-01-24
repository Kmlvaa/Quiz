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

		[HttpGet]
		public IActionResult Get()
		{
			var quizzes = _appDbContext.Quizzes.ToList();
			if(quizzes is null) { return NotFound(); }

			return Ok(quizzes);
		}
		public IActionResult GetById(int id)
		{
			var quiz = _appDbContext.Quizzes.FirstOrDefault(x => x.Id == id);
			if (quiz != null) return NotFound();

			var question = _appDbContext.Questiones.Where(x => x.QuizId == id).ToList();
			if (question != null) return NotFound();

			var dto = new QuizGetDetailsDto()
			{
				Name = quiz.Name,
				CreationDate = quiz.CreationDate,
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
		[HttpPut]
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
