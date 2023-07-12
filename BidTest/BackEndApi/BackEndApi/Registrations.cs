using BackEndApi.Interfaces;
using BackEndApi.Repositories;
using BackEndApi.Services;

namespace BackEndApi
{
	public static class Registrations
	{
		public static IServiceCollection RegisterDependencies (this IServiceCollection serviceCollections)
		{
			serviceCollections.AddTransient<IPersonService, PersonService> ();
			return serviceCollections;
		}

		public static IServiceCollection RegisterInfrastructureServices (this IServiceCollection serviceCollections, IConfiguration configuration)
		{
			serviceCollections.AddTransient<IPersonRepository> ( opts => new PersonRepository(configuration));
			return serviceCollections;
		}
	}
}

