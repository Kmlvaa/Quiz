﻿namespace Quizz.DTO
{
	public class QuestionPostDto
	{
		public string Name { get; set; }
		public decimal Points { get; set; }
		public List<OptionPostDto> OptionPost { get; set; }
	}
}
