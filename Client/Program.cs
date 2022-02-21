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
		builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));
		var httpClient = new HttpClient();
		builder.Services.AddSingleton(httpClient);
		builder.Services.AddSingleton(new ApiClient(httpClient));
		builder.Services.AddBlazoredLocalStorage();

		await builder.Build().RunAsync();
	}
}