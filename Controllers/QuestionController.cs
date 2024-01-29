using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quizz.Data;
using Quizz.DTO;
using Quizz.Entities;

namespace Quizz.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuestionController : ControllerBase
	{
		private readonly AppDbContext _appDbContext;
		private readonly IMapper _mapper;
		public QuestionController(AppDbContext dbContext, IMapper mapper)
		{
			_appDbContext = dbContext;
			_mapper = mapper;
		}
		[HttpPut("{id}")]
		[Authorize(Roles = "Admin")]
		public IActionResult Put(int id,[FromBody] QuestionPutDto dto)
		{
			var question = _appDbContext.Questiones.FirstOrDefault(x => x.Id == id);
			if (question == null) return NotFound();

			_mapper.Map(dto, question);

			_appDbContext.Update(question);
			_appDbContext.SaveChanges();

			return Ok();
		}

	}
}
