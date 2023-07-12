using System.Text.Json;
using BackEndApi.Models;

namespace BackEndApi.Utilities
{
	public class ReadJsonFileWithSystemTextJson
	{
		private readonly string _sampleJsonFilePath;
		private readonly JsonSerializerOptions _options = new () {
			PropertyNameCaseInsensitive = true
		};

		public ReadJsonFileWithSystemTextJson(string sampleJsonFilePath)
		{
			_sampleJsonFilePath = sampleJsonFilePath;
		}

		public IEnumerable<Person> UseFileOpenReadTextWithSystemTextJson ()
		{
			using StreamReader sr = new StreamReader(_sampleJsonFilePath);
			while (!sr.EndOfStream)
			{
				var fileRow = sr.ReadLine();
				var person = JsonSerializer.Deserialize<Person>(fileRow, _options);
				yield return person;
			}
		}

		public async Task<IEnumerable<Person>> UseFileOpenReadTextWithSystemTextJsonAsync (CancellationToken cancellationToken)
		{
			return await Task.Run (() => UseFileOpenReadTextWithSystemTextJson (), cancellationToken);
		}
	}
}

