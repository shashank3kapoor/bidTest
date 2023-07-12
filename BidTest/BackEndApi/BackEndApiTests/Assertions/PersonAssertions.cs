using BackEndApi.Models;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace BackEndApiTests.Assertions
{
	public class PersonAssertions : ReferenceTypeAssertions<IEnumerable<Person>, PersonAssertions> {
		public PersonAssertions (IEnumerable<Person> subject) : base (subject)
		{
		}

		protected override string Identifier => nameof (Person);

		public PersonAssertions HaveSpecificNumberOfPeople (
		    int expectedNumberOfPeople
		    , string? because = null
		    , params object [] becauseArgs)
		{
			var totalPeople = Subject?.ToList ().Count;

			Execute.Assertion
			    .ForCondition (totalPeople == expectedNumberOfPeople)
			    .BecauseOf (because, becauseArgs)
			    .FailWith ("Expected number of People:{reason}, but got {0}", totalPeople);

			return this;
		}

		public PersonAssertions BeEquivalentTo (
		    IEnumerable<Person> expectedPerson
		    , string? because = null
		    , params object [] becauseArgs)
		{
			var Person = Subject.ToList ();
			var expectedPersonList = expectedPerson.ToList ();
			var matched = Person.Count == expectedPersonList.Count;

			Execute.Assertion
			    .ForCondition (matched)
			    .BecauseOf (because, becauseArgs)
			    .FailWith ("Expected specific number of People:{reason}, but got {0}: {1}",
				Person.Count, Person);

			return this;
		}
	}

	public static class PersonAssertionExtensions {
		public static PersonAssertions Should (this IEnumerable<Person> Person)
		    => new PersonAssertions (Person);
	}
}

