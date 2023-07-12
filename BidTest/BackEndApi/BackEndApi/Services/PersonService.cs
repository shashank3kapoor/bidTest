using BackEndApi.Interfaces;
using BackEndApi.Models;

namespace BackEndApi.Services
{
	public class PersonService : IPersonService
	{
		private readonly IPersonRepository _personRepository;

		public PersonService(IPersonRepository personRepository)
		{
			_personRepository = personRepository;
		}

		public async Task AddPerson(Person person, CancellationToken cancellationToken)
		{
			await _personRepository.AddPerson (person, cancellationToken);
		}

		public async Task<IEnumerable<Person>> GetAllPeople(CancellationToken cancellationToken)
		{
			return await _personRepository.GetAllPeople (cancellationToken);
		}
	}
}

