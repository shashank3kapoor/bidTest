using BackEndApi.Models;

namespace BackEndApi.Interfaces
{
	public interface IPersonService
	{
		/// <summary>
		/// Add Person.
		/// </summary>
		/// <param name="person">Person Info</param>
		/// <returns></returns>
		Task AddPerson (Person person, CancellationToken cancellationToken);

		/// <summary>
		/// Get All Records.
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<Person>> GetAllPeople (CancellationToken cancellationToken);
	}
}

