namespace Quizz.DTO
{
	public class QuestionGetDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Points { get; set; }
		public List<OptionGetDto> Option { get; set; }
	}
}
