namespace Quizz.DTO.QuizDTO
{
    public class QuizPostDto
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime CreationDate { get; set; }
		public List<QuestionPostDto> QuestionPost { get; set; }
	}
}
