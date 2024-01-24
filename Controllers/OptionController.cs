using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quizz.Data;
using Quizz.DTO;

namespace Quizz.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OptionController : ControllerBase
	{
		private readonly AppDbContext _appDbContext;
		public OptionController(AppDbContext dbContext)
		{
			_appDbContext = dbContext;
		}
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] OptionPutDto dto)
		{
			var option = _appDbContext.Options.FirstOrDefault(x => x.Id == id);
			if (option == null) return NotFound();

			option.Name = dto.Name;
			option.IsCorrect = dto.IsCorrect;

			_appDbContext.Update(option);
			_appDbContext.SaveChanges();

			return Ok();
		}
	}
}
