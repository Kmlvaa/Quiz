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
		[Authorize]
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
			var quiz = _appDbContext.Quizzes.Include(x => x.Questions).ThenInclude(x => x.Options).SingleOrDefault(x => x.Id == id);

			if (quiz == null) return NotFound();

			var dto = _mapper.Map<QuizGetDetailsDto>(quiz);

			return Ok(dto);
		}
		[HttpPost("Post")]
		[Authorize(Roles = "Admin")]
		public IActionResult Post([FromBody] QuizPostDto dto)
		{
			var quiz = _mapper.Map<QuizPostDto, Quiz>(dto);

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
