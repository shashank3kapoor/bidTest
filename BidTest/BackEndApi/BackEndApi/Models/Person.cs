namespace BackEndApi.Models
{
	public class Person
	{
		public Guid PersonId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public Person ()
		{
			PersonId = Guid.NewGuid();
			FirstName = ""; //Default empty to avoid null
			LastName = ""; //Default empty to avoid null
		}
	}
}

