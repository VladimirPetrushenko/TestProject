namespace MyClient.Models.Persons
{
    public class PersonReadDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public bool IsBlock { get; set; }
    }
}
