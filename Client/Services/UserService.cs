using System.Net.Http.Json;
using Client.Models;

namespace Client.Services; 

public interface IUserService {
	Task<CurrentUser> GetCurrentUserAsync();
}

public class UserService : IUserService {
	private readonly HttpClient _httpClient;

	public UserService(HttpClient httpClient) => _httpClient = httpClient;

	public async Task<CurrentUser> GetCurrentUserAsync() => (await _httpClient.GetFromJsonAsync<CurrentUser>("data/current_user.json"))!;
}