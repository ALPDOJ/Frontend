using Client.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.Account.Center
{
	public partial class Articles
	{
		[Parameter]
		public IList<ListItemDataType> List { get; set; }
	}
}