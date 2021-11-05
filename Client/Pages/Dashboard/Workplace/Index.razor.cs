using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.Dashboard.Workplace; 

public partial class Index {
	private readonly EditableLink[] _links = {
		new() { Title = "Operation 1", Href = "" },
		new() { Title = "Operation 2", Href = "" },
		new() { Title = "Operation 3", Href = "" },
		new() { Title = "Operation 4", Href = "" },
		new() { Title = "Operation 5", Href = "" },
		new() { Title = "Operation 6", Href = "" }
	};

	private ActivitiesType[] _activities = { };

	private NoticeType[] _projectNotice = { };

	[Inject]
	public IProjectService ProjectService { get; set; }

	protected override async Task OnInitializedAsync() {
		await base.OnInitializedAsync();
		_projectNotice = await ProjectService.GetProjectNoticeAsync();
		_activities = await ProjectService.GetActivitiesAsync();
	}
}