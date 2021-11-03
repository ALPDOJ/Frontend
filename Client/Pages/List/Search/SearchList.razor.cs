using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.List
{
	public partial class SearchList
	{
		private readonly IList<TabPaneItem> _tabList = new List<TabPaneItem> {
		new() { Key = "articles", Tab = "Articles" },
		new() { Key = "projects", Tab = "Projects" },
		new() { Key = "applications", Tab = "Applications" }
	};

		[Inject]
		protected NavigationManager NavigationManager { get; set; }

		private string GetTabKey()
		{
			string url = NavigationManager.Uri.TrimEnd('/');
			string key = url.Substring(url.LastIndexOf('/') + 1);
			return key;
		}

		private void HandleTabChange(string key)
		{
			string url = NavigationManager.Uri.TrimEnd('/');
			url = url.Substring(0, url.LastIndexOf('/'));
			NavigationManager.NavigateTo($"{url}/{key}");
		}
	}
}