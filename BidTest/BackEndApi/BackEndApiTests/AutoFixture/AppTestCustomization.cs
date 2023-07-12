using AutoFixture;
using AutoFixture.AutoMoq;
using BackEndApi;
using BackEndApiTests.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IServiceCollection = Microsoft.Extensions.DependencyInjection.IServiceCollection;
using ServiceCollection = Microsoft.Extensions.DependencyInjection.ServiceCollection;

namespace BackEndApiTests.AutoFixture
{
	internal class AppTestCustomization : ICustomization {
		private readonly IServiceCollection _collection;

		public AppTestCustomization ()
		{
			var myConfiguration = new Dictionary<string, string>
			{
			    {"FileName", "person-test"}
			};

			var configuration = new ConfigurationBuilder ()
			    .AddInMemoryCollection (myConfiguration)
			    .Build ();

			_collection = new ServiceCollection ()
				.RegisterDependencies ()
				.RegisterInfrastructureServices(configuration)
				.AddTestDependencies(configuration)
				.AddLogging (lg => {
					lg.ClearProviders ();
					lg.AddConsole ();
				});
		}

		public void Customize (IFixture fixture)
		{
			var specimenBuilder = new ServiceProviderSpecimenBuilder (_collection.BuildServiceProvider ());
			fixture.Customize (new AutoMoqCustomization {
				Relay = specimenBuilder
			});

			fixture.ResidueCollectors.Add (specimenBuilder);
		}
	}
}

