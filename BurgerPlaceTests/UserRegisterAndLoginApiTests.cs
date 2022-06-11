using BurgerPlace.Context;

namespace BurgerPlaceTests
{
    public class UserRegisterAndLoginApiTests : IClassFixture<WebApplicationFactory<BurgerPlace.Program>>
    {
        readonly HttpClient _client;
        private BurgerPlaceContext context = new BurgerPlaceContext();

        public UserRegisterAndLoginApiTests(WebApplicationFactory<BurgerPlace.Program> application)
        {
            _client = application.CreateClient();
        }


        [Fact]
        public async void Should_Succeed_CreateNewUser()
        {
            // Arrange
            RegisterRequest registerRequest = new RegisterRequest();
            registerRequest.email = Faker.StringFaker.AlphaNumeric(8)+"@gmail.com";
            registerRequest.name = "JohnUnitTest1";
            registerRequest.surname = "John";
            // generate username
            registerRequest.username = Faker.StringFaker.AlphaNumeric(10);
            registerRequest.password = "12345678";
            var json = JsonConvert.SerializeObject(registerRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            // Act
            var response = await _client.PostAsync("api/User/register", data);
            // Cleaning
            var user = await context.Users.Where(i => i.Username == registerRequest.username).FirstOrDefaultAsync();
            if (user != null) context.Users.Remove(user);
            context.SaveChanges();
            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.Created);
        }
        [Fact]
        public async void Should_Fail_RegisterOnEmailInDB()
        {
            // Arrange
            RegisterRequest registerRequest = new RegisterRequest();
            registerRequest.email = "example@example.org";
            registerRequest.name = "JohnUnitTest2";
            registerRequest.surname = "John";
            // generate username
            registerRequest.username = Faker.StringFaker.AlphaNumeric(10);
            registerRequest.password = "12345678";
            var json = JsonConvert.SerializeObject(registerRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            // Act
            // Register user with this data
            await _client.PostAsync("api/User/register", data);
            // Register same user with different username but same email
            registerRequest.username = Faker.StringFaker.AlphaNumeric(10);
            json = JsonConvert.SerializeObject(registerRequest);
            data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/User/register", data);
            // Cleaning
            var user = await context.Users.Where(i => i.Username == registerRequest.username).FirstOrDefaultAsync();
            if (user != null) context.Users.Remove(user);
            context.SaveChanges();
            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async void Should_Register_And_Login()
        {
            // Arrange
            var username = Faker.StringFaker.AlphaNumeric(10);
            var password = Faker.StringFaker.AlphaNumeric(8);
            RegisterRequest registerRequest = new RegisterRequest();
            registerRequest.email = Faker.StringFaker.AlphaNumeric(8) + "@gmail.com";
            registerRequest.name = "JohnUnitTest3";
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
            // Cleaning
            var user = await context.Users.Where(i => i.Username == registerRequest.username).FirstOrDefaultAsync();
            if (user != null) context.Users.Remove(user);
            context.SaveChanges();

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }
    }
}