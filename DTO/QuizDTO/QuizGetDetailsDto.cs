namespace Quizz.DTO.QuizDTO
{
	public class QuizGetDetailsDto
	{
		public string Name { get; set; }
		public DateTime CreationDate { get; set; }
		public List<QuestionGetDto> Question { get; set; }
	}
}
