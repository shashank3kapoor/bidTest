using BackEndApiTests.Interfaces;
using BackEndApiTests.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackEndApiTests.AutoFixture
{
	public static class RegisterTestDependencies {
		
		public static IServiceCollection AddTestDependencies (this IServiceCollection serviceCollections, IConfiguration configuration)
		{
			serviceCollections.AddTransient<ITestDataCleanUp> (opts => new TestDataCleanUp (configuration));
			return serviceCollections;
		}
	}
}

