using AntDesign.ProLayout;
using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.Profile; 

public partial class Advanced {
	private readonly IList<TabPaneItem> _tabList = new List<TabPaneItem> {
		new() { Key = "detail", Tab = "Details" },
		new() { Key = "rules", Tab = "Rules" }
	};

	private AdvancedProfileData _data = new();

	[Inject]
	protected IProfileService ProfileService { get; set; }

	protected override async Task OnInitializedAsync() {
		await base.OnInitializedAsync();
		_data = await ProfileService.GetAdvancedAsync();
	}
}