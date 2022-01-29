using AntDesign.ProLayout;
using Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Client;

public class Program {
	public static async Task Main(string[] args) {
		var builder = WebAssemblyHostBuilder.CreateDefault(args);
		builder.RootComponents.Add<App>("#app");

		builder.Services.AddAntDesign();
		builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));
		builder.Services.AddScoped<IChartService, ChartService>();
		builder.Services.AddScoped<IProjectService, ProjectService>();
		builder.Services.AddScoped<IUserService, UserService>();
		builder.Services.AddScoped<IAccountService, AccountService>();
		builder.Services.AddScoped<IProfileService, ProfileService>();
		builder.Services.AddSingleton(new Api(new HttpClient()));

		await builder.Build().RunAsync();
	}
}