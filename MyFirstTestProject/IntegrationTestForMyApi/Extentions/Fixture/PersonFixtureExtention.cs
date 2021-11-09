using AutoFixture;
using MyClient.Models.Persons;

namespace IntegrationTestForMyApi.Extentions.Fixture
{
    public static class AddPersonFixtureExtention
    {
        public static AddPerson CreateValideAddPerson(this IFixture fixture)
        {
            var firstName = fixture.Create<string>().Substring(0, 15);
            var lastName = fixture.Create<string>().Substring(0, 15);

            return fixture.Build<AddPerson>()
                .With(p => p.FirstName, firstName)
                .With(p => p.LastName, lastName)
                .With(p => p.IsActive, true)
                .Create();
        }
        public static AddPerson CreateAddModelWhithoutLastName(this IFixture fixture) =>
            fixture.Build<AddPerson>()
                .With(p => p.FirstName, "Vladimir")
                .Without(p => p.LastName)
                .With(p => p.IsActive, true)
                .Create();

        public static AddPerson CreateAddModelWhithoutIsActive(this IFixture fixture) =>
            fixture.Build<AddPerson>()
                .With(p => p.FirstName, "Vladimir")
                .With(p => p.LastName, "Petrushenko")
                .With(p => p.IsActive, false)
                .Create();
    }
}
