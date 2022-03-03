using AntDesign.ProLayout;
using Blazored.LocalStorage;
using Client.Api;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

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
		builder.Services.AddSingleton<ErrorHandler>();
		builder.Services.AddBlazoredLocalStorage();

		await builder.Build().RunAsync();
	}
}