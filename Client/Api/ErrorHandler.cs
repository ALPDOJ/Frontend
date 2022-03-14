using AntDesign;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace Client.Api;

public class ErrorHandler {
	public ErrorHandler(NavigationManager navigator, MessageService messageService) {
		Navigator = navigator;
		MessageService = messageService;
	}

	private NavigationManager Navigator { get; }

	private MessageService MessageService { get; }

	public bool Handle(ApiException exception, params int[] statusCodes) {
		if (statusCodes.Length > 0 && !statusCodes.Contains(exception.StatusCode))
			return false;
		return exception.StatusCode switch {
			400 => HandleBadRequest(exception),
			401 => HandleUnauthorized(exception),
			403 => HandleForbidden(exception),
			500 => HandleInternalError(exception),
			_   => false
		};
	}

	public bool Handle(Exception exception) {
		switch (exception) {
			case ApiException ex: return Handle(ex);
			case NotImplementedException ex:
				string text = string.IsNullOrEmpty(ex.Message) ? "功能尚未实现，敬请期待" : ex.Message;
				var _ = MessageService.Info(text);
				return true;
			default:
				LogToConsole(exception);
				Message(exception);
				return true;
		}
	}

	public static void LogToConsole(Exception exception) => Console.WriteLine(JsonConvert.SerializeObject(exception, Formatting.Indented));

	public void Message(Exception exception) {
		var _ = MessageService.Error(exception.Message);
	}

	private bool HandleBadRequest(ApiException exception) {
		if (exception.StatusCode != 400)
			return false;
		var _ = MessageService.Error(exception.Response);
		return true;
	}

	private bool HandleUnauthorized(ApiException exception) {
		if (exception.StatusCode != 401)
			return false;
		var _ = MessageService.Error("会话已过期，请重新登录");
		Navigator.NavigateTo("/login");
		return true;
	}

	private bool HandleForbidden(ApiException exception) {
		if (exception.StatusCode != 403)
			return false;
		Navigator.NavigateTo("/exception?status=403");
		return true;
	}

	private bool HandleInternalError(ApiException exception) {
		if (exception.StatusCode != 500)
			return false;
		LogToConsole(exception);
		Message(exception);
		return true;
	}
}