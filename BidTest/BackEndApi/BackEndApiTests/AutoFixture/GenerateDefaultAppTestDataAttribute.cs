using AutoFixture;
using AutoFixture.Xunit2;

namespace BackEndApiTests.AutoFixture
{
	public class GenerateDefaultAppTestDataAttribute : AutoDataAttribute {
		public GenerateDefaultAppTestDataAttribute () : base (GetDefaultFixture)
		{
		}

		public static IFixture GetDefaultFixture ()
		{
			return new Fixture ()
			    .Customize (new AppTestCustomization ());
		}
	}
}

