using AntDesign.ProLayout;
using Blazored.LocalStorage;
using Client.Api;
using Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Client;

public class Program {
	public static async Task Main(string[] args) {
		var builder = WebAssemblyHostBuilder.CreateDefault(args);
		builder.RootComponents.Add<App>("#app");

		builder.Services.AddAntDesign();
		builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));
		builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
		builder.Services.AddScoped<IChartService, ChartService>();
		builder.Services.AddScoped<IProjectService, ProjectService>();
		builder.Services.AddScoped<IUserService, UserService>();
		builder.Services.AddScoped<IAccountService, AccountService>();
		builder.Services.AddScoped<IProfileService, ProfileService>();
		builder.Services.AddSingleton(new ApiClient(new HttpClient()));
		builder.Services.AddBlazoredLocalStorage();

		await builder.Build().RunAsync();
	}
}