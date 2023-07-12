using BackEndApiTests.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BackEndApiTests.Utilities
{
	public class TestDataCleanUp : ITestDataCleanUp {
		private readonly string FileName;

		public TestDataCleanUp(IConfiguration configuration)
		{
			FileName = configuration.GetValue<string> ("FileName");
		}

		public void RemoveFile ()
		{
			if (File.Exists (FileName)) {
				File.Delete (FileName);
			}
		}
	}
}

