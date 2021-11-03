using AntDesign;
using Client.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.Account.Center
{
	public partial class Applications
	{
		private readonly ListGridType _listGridType = new()
		{
			Gutter = 24,
			Column = 4
		};

		[Parameter]
		public IList<ListItemDataType> List { get; set; }
	}
}