using AutoMapper;
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
		private readonly IMapper _mapper;
		public OptionController(AppDbContext dbContext, IMapper mapper)
		{
			_appDbContext = dbContext;
			_mapper = mapper;
		}
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] OptionPutDto dto)
		{
			var option = _appDbContext.Options.FirstOrDefault(x => x.Id == id);
			if (option == null) return NotFound();

			_mapper.Map(dto, option);

			_appDbContext.Update(option);
			_appDbContext.SaveChanges();

			return Ok();
		}
	}
}
