using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TainaTech.Api;
using TainaTech.API.IntegrationTests.Base;
using TainaTech.Application.Features.Persons.Queries.GetPersonsList;
using Xunit;

namespace TainaTech.API.IntegrationTests.Controllers
{
    public class PersonControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public PersonControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnsSuccessResult()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/person/all");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<PersonListVm>>(responseString);

            Assert.IsType<List<PersonListVm>>(result);
            Assert.NotEmpty(result);
        }
    }
}
