using Microsoft.AspNetCore.Components;

namespace Client.Utils;

public static class NavigationManagerExtension {
	public static void NavigateTo(this NavigationManager manager, Uri uri, NavigationOptions options) => manager.NavigateTo(uri.ToString(), options);

	public static void NavigateTo(this NavigationManager manager, Uri uri) => manager.NavigateTo(uri.ToString());
}