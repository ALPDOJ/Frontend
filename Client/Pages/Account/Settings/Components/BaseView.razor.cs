using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.Account.Settings
{
	public partial class BaseView
	{
		private CurrentUser _currentUser = new();

		[Inject]
		protected IUserService UserService { get; set; }

		private void HandleFinish() { }

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			_currentUser = await UserService.GetCurrentUserAsync();
		}
	}
}