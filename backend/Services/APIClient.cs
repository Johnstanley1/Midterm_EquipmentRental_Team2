using System.Security.Claims;

namespace Midterm_EquipmentRental_Team2.Services
{
    public class APIClient
    {
        private readonly HttpClient _httpClient;
        private readonly JWTService _jwtService;

        public APIClient(HttpClient httpClient, JWTService jwtService) { 
            _httpClient = httpClient;
            _jwtService = jwtService;
        }

        private void AttachJwtToken(ClaimsPrincipal user)
        {

            var token = _jwtService.GenerateToken(user);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        }


        public async Task<(string, string)> GetProtectedDataAsync(string path, ClaimsPrincipal principal)
        {
            AttachJwtToken(principal);
            var requestUri = new Uri(_httpClient.BaseAddress!, path);
            var response = await _httpClient.GetAsync(requestUri);
            var body = await response.Content.ReadAsStringAsync();
            return (response.StatusCode.ToString(), body);

        }
    }
}
