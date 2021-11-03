using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.Profile
{
	public partial class Basic
	{
		private BasicProfileDataType _data = new();

		[Inject]
		protected IProfileService ProfileService { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			_data = await ProfileService.GetBasicAsync();
		}
	}
}