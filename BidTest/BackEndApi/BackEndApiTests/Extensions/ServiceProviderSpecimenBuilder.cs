using AutoFixture.Kernel;

namespace BackEndApiTests.Extensions
{
	public class ServiceProviderSpecimenBuilder : ISpecimenBuilder
	{
		private readonly IServiceProvider _provider;

		public ServiceProviderSpecimenBuilder (IServiceProvider provider)
		{
			_provider = provider;
		}

		public object? Create (object request, ISpecimenContext context)
		{
			return request is not Type type ? new NoSpecimen () : Resolve (type);
		}

		private object? Resolve (Type type)
		{
			return _provider.GetService (type);
		}
	}
}

