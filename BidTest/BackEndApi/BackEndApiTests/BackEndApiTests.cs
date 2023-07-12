using AutoFixture.Xunit2;
using BackEndApi.Controllers;
using BackEndApi.Models;
using BackEndApiTests.Assertions;
using BackEndApiTests.AutoFixture;
using BackEndApiTests.Interfaces;

namespace BackEndApiTests;

public class BackEndApiTests
{

	[Theory, GenerateDefaultAppTestData]
	public async Task GIVEN_person_added_WHEN_getAllPersons_requested_THEN_people_returned (
		[Greedy] PersonController sut,
		ITestDataCleanUp testDataCleanUp)
	{
		// Given
		var addedPerson = new Person {
			FirstName = "Fname1",
			LastName = "Lname1"
		};
		await sut.AddPerson (addedPerson, CancellationToken.None);

		var addedPerson2 = new Person {
			FirstName = "Fname2",
			LastName = "Lname2"
		};
		await sut.AddPerson (addedPerson2, CancellationToken.None);

		// When
		var act = await sut.GetAllPeople (CancellationToken.None);

		// Then
		act.Should ().HaveSpecificNumberOfPeople (2).BeEquivalentTo (new []
		{
			addedPerson,
			addedPerson2
		});

		testDataCleanUp.RemoveFile ();
	}
}
