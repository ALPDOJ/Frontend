using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.List; 

public partial class Articles {
	private readonly string[] _defaultOwners = { "wzj", "wjh" };

	private readonly ListFormModel _model = new();

	private readonly Owner[] _owners = {
		new() { Id = "wzj", Name = "Myself" },
		new() { Id = "wjh", Name = "Wu Jiahao" },
		new() { Id = "zxx", Name = "Zhou Xingxing" },
		new() { Id = "zly", Name = "Zhao Liying" },
		new() { Id = "ym", Name = "Yao Ming" }
	};

	private IList<ListItemDataType> _fakeList = new List<ListItemDataType>();

	[Inject]
	public IProjectService ProjectService { get; set; }

	private void SetOwner() { }

	protected override async Task OnInitializedAsync() {
		await base.OnInitializedAsync();
		_fakeList = await ProjectService.GetFakeListAsync(8);
	}
}