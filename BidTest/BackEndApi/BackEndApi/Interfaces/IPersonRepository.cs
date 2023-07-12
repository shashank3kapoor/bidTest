using BackEndApi.Models;

namespace BackEndApi.Interfaces
{
	public interface IPersonRepository
	{
		/// <summary>
		/// Add Person to repository.
		/// </summary>
		/// <param name="person">Person Info</param>
		Task AddPerson (Person person, CancellationToken cancellationToken);

		/// <summary>
		/// Get All Records.
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<Person>> GetAllPeople (CancellationToken cancellationToken);
	}
}

