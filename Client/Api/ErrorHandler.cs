using Microsoft.AspNetCore.Components;

namespace Client.Api;

public class ErrorHandler {
	private ApiClient Api { get; }

	private NavigationManager Navigator { get; }

	public ErrorHandler(ApiClient api, NavigationManager navigator) {
		Api = api;
		Navigator = navigator;
	}

	public void Handle(ApiException exception, params int[] statusCodes) {
		if (statusCodes.Length > 0 && !statusCodes.Contains(exception.StatusCode))
			return;
		switch (exception.StatusCode) {
			case 401:
				HandleUnauthorized(exception);
				break;
			case 403:
				HandleForbidden(exception);
				break;
		}
	}

	private void HandleUnauthorized(ApiException exception) {
		if (exception.StatusCode == 401)
			Navigator.NavigateTo("/login");
	}

	private void HandleForbidden(ApiException exception) {
		if (exception.StatusCode != 403)
			return;
		Navigator.NavigateTo("/exception?status=403");
	}
}