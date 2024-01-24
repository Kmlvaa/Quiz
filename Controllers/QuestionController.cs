using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quizz.Data;
using Quizz.DTO;

namespace Quizz.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuestionController : ControllerBase
	{
		private readonly AppDbContext _appDbContext;
		public QuestionController(AppDbContext dbContext)
		{
			_appDbContext = dbContext;
		}
		[HttpPut]
		public IActionResult Put(int id,[FromBody] QuestionPutDto dto)
		{
			var question = _appDbContext.Questiones.FirstOrDefault(x => x.Id == id);
			if (question == null) return NotFound();

			question.Name = dto.Name;
			question.Points = dto.Points;

			_appDbContext.Update(question);
			_appDbContext.SaveChanges();

			return Ok();
		}

	}
}
