using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace PetNameGeneratorAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PetNameController : ControllerBase
	{
		private static readonly List<string> DogNames = new List<string> { "Buddy", "Max", "Charlie", "Rocky", "Rex" };
		private static readonly List<string> CatNames = new List<string> { "Whiskers", "Mittens", "Luna", "Simba", "Tiger" };
		private static readonly List<string> BirdNames = new List<string> { "Tweety", "Sky", "Chirpy", "Raven", "Sunny" };

		[HttpPost("generate")]
		public IActionResult GeneratePetName([FromBody] PetNameRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.AnimalType))
			{
				return BadRequest(new { Message = "Animal type cannot be empty." });
			}

			List<string> names;
			switch (request.AnimalType.ToLower())
			{
				case "dog":
					names = DogNames;
					break;
				case "cat":
					names = CatNames;
					break;
				case "bird":
					names = BirdNames;
					break;
				default:
					return BadRequest(new { Message = "Invalid animal type. Choose 'dog', 'cat', or 'bird'." });
			}

			Random rnd = new Random();
			string petName = names[rnd.Next(names.Count)];

			if (request.TwoPart.HasValue && request.TwoPart.Value)
			{
				petName +=  names[rnd.Next(names.Count)];
			}

			return Ok(new { PetName = petName });
		}
	}

	public class PetNameRequest
	{
		public string AnimalType { get; set; }
		public bool? TwoPart { get; set; }
	}
}
