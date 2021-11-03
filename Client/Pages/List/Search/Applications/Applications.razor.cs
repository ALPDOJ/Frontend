using AntDesign;
using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.List
{
	public partial class Applications
	{
		private readonly ListGridType _listGridType = new()
		{
			Gutter = 16,
			Xs = 1,
			Sm = 2,
			Md = 3,
			Lg = 3,
			Xl = 4,
			Xxl = 4
		};

		private readonly ListFormModel _model = new();

		private readonly IList<string> _selectCategories = new List<string>();

		private IList<ListItemDataType> _fakeList = new List<ListItemDataType>();

		[Inject]
		public IProjectService ProjectService { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			_fakeList = await ProjectService.GetFakeListAsync(8);
		}
	}
}