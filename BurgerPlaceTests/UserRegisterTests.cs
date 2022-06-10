namespace BurgerPlaceTests
{
    public class UserRegisterTests : IClassFixture<WebApplicationFactory<BurgerPlace.Program>>
    {
        readonly HttpClient _client;

        public UserRegisterTests(WebApplicationFactory<BurgerPlace.Program> application)
        {
            _client = application.CreateClient();
        }


        [Fact]
        public async void Should_Succeed_CreateNewUser()
        {
            // Arrange
            RegisterRequest registerRequest = new RegisterRequest();
            registerRequest.email = "example@example.org";
            registerRequest.name = "John";
            registerRequest.surname = "John";
            // generate username
            registerRequest.username = Faker.StringFaker.AlphaNumeric(10);
            registerRequest.password = "12345678";
            var json = JsonConvert.SerializeObject(registerRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            // Act
            var response = await _client.PostAsync("api/User/register", data);

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }
        
        [Fact]
        public async void Should_Register_And_Login()
        {
            // Arrange
            var username = Faker.StringFaker.AlphaNumeric(10);
            var password = Faker.StringFaker.AlphaNumeric(8);
            RegisterRequest registerRequest = new RegisterRequest();
            registerRequest.email = "example@example.org";
            registerRequest.name = "Johny";
            registerRequest.surname = "Test";
            registerRequest.username = username;
            registerRequest.password = password;
            LoginRequest loginRequest = new();
            loginRequest.username = username;
            loginRequest.password = password;

            // Act
            // Register
            var json = JsonConvert.SerializeObject(registerRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            await _client.PostAsync("api/User/register", data);
            // Login
            json = JsonConvert.SerializeObject(loginRequest);
            data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/User/login", data);

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }
    }
}