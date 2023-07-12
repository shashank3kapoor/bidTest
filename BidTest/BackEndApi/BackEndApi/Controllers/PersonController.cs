using BackEndApi.Interfaces;
using BackEndApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEndApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
	private readonly ILogger<PersonController> _logger;
	private readonly IPersonService _PersonService;

	public PersonController (ILogger<PersonController> logger
	    , IPersonService PersonService)
	{
		_logger = logger;
		_PersonService = PersonService;
	}

	[HttpGet]
	[Route ("/GetAllPeople")]
	public async Task<IEnumerable<Person>> GetAllPeople (CancellationToken cancellationToken)
	{
		try {
			return await _PersonService.GetAllPeople (cancellationToken);
		} catch (Exception ex) {
			_logger.LogError ("Error occurred: {errorMessage}", ex.Message);
			throw;
		}
	}

	[HttpPost]
	public async Task AddPerson (Person Person, CancellationToken cancellationToken)
	{
		try {
			await _PersonService.AddPerson (Person, cancellationToken);
		} catch (Exception ex) {
			_logger.LogError ("Error occurred: {errorMessage}", ex.Message);
			throw;
		}
	}
}

