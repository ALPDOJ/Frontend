using Client.Models;
using System.Net.Http.Json;

namespace Client.Services
{
	public interface IProfileService
	{
		Task<BasicProfileDataType> GetBasicAsync();

		Task<AdvancedProfileData> GetAdvancedAsync();
	}

	public class ProfileService : IProfileService
	{
		private readonly HttpClient _httpClient;

		public ProfileService(HttpClient httpClient) => _httpClient = httpClient;

		public async Task<BasicProfileDataType> GetBasicAsync() => (await _httpClient.GetFromJsonAsync<BasicProfileDataType>("data/basic.json"))!;

		public async Task<AdvancedProfileData> GetAdvancedAsync() => (await _httpClient.GetFromJsonAsync<AdvancedProfileData>("data/advanced.json"))!;
	}
}