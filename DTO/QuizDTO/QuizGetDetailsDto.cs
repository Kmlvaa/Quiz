namespace Quizz.DTO.QuizDTO
{
	public class QuizGetDetailsDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime CreationDate { get; set; }
		public List<QuestionGetDto> Questions { get; set; }
	}
}
