using AntDesign.ProLayout;
using Blazored.LocalStorage;
using Client.Api;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Client;

public class Program {
	public static async Task Main(string[] args) {
		var builder = WebAssemblyHostBuilder.CreateDefault(args);
		builder.RootComponents.Add<App>("#app");

		builder.Services.AddAntDesign();
		builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("proSettings"));
		var httpClient = new HttpClient();
		builder.Services.AddSingleton(httpClient);
		var apiClient = new ApiClient(httpClient) {
			BaseUrl = builder.Configuration["baseUrl"]!
		};
		builder.Services.AddSingleton(apiClient);
		builder.Services.AddScoped<ErrorHandler>();
		builder.Services.AddBlazoredLocalStorage();
		builder.Services.AddScoped<LocalStorageManager>();

		JsonConvert.DefaultSettings = () => new JsonSerializerSettings {
			Converters = new JsonConverter[] {
				new StringEnumConverter(new CamelCaseNamingStrategy())
			}
		};

		await builder.Build().RunAsync();
	}
}