﻿namespace Quizz.DTO
{
	public class QuestionPutDto
	{
		public string Name { get; set; }
		public decimal Points { get; set; }
		public List<OptionPutDto> Options { get; set; }
	}
}
