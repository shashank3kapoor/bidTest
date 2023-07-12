using BackEndApi.Interfaces;
using BackEndApi.Models;
using BackEndApi.Utilities;

namespace BackEndApi.Repositories
{
	public class PersonRepository : IPersonRepository
	{
		private readonly string FileName;
		private readonly ReadJsonFileWithSystemTextJson _readJsonFileWithSystemTextJson;

		public PersonRepository(IConfiguration configuration)
		{
			FileName = configuration.GetValue<string>("FileName");
			_readJsonFileWithSystemTextJson = new ReadJsonFileWithSystemTextJson (FileName);
		}

		public async Task AddPerson(Person person, CancellationToken cancellationToken)
		{
			await NativeJsonFileUtils.StreamWriteAsync (person, FileName, cancellationToken);
		}

		public async Task<IEnumerable<Person>> GetAllPeople(CancellationToken cancellationToken)
		{
			return await _readJsonFileWithSystemTextJson.UseFileOpenReadTextWithSystemTextJsonAsync (cancellationToken);
		}
	}
}

