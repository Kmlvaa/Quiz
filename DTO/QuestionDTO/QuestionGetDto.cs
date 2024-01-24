namespace Quizz.DTO
{
	public class QuestionGetDto
	{
		public string Name { get; set; }
		public decimal Points { get; set; }
		public List<OptionGetDto> Option { get; set; }
	}
}
