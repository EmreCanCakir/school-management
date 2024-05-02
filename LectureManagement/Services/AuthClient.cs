namespace LectureManagement.Services
{
    public class AuthClient
    {
        private readonly HttpClient client;

        public AuthClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<string> Login(string userName, string Password)
        {
            var response = await client.PostAsJsonAsync("Auth/Login", new { userName, Password });
            response.EnsureSuccessStatusCode();
            var token = await response.Content.ReadAsStringAsync();
            return token;
        }
    }
}
