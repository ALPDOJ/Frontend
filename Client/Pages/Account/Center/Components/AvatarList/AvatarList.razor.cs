using Microsoft.AspNetCore.Components;

namespace Client.Pages.Account.Center; 

public partial class AvatarList {
	[Parameter]
	public RenderFragment ChildContent { get; set; }
}