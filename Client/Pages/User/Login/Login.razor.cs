using AntDesign;
using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Components;

namespace Client.Pages.User
{
	public partial class Login
	{
		private readonly LoginParamsType _model = new();

		[Inject]
		public NavigationManager NavigationManager { get; set; }

		[Inject]
		public IAccountService AccountService { get; set; }

		[Inject]
		public MessageService Message { get; set; }

		public void HandleSubmit()
		{
			if (_model.UserName == "admin" && _model.Password == "ant.design")
			{
				NavigationManager.NavigateTo("/");
				return;
			}

			if (_model.UserName == "user" && _model.Password == "ant.design")
				NavigationManager.NavigateTo("/");
		}

		public async Task GetCaptcha()
		{
			string captcha = await AccountService.GetCaptchaAsync(_model.Mobile);
			await Message.Success($"Verification code validated successfully! The verification code is: {captcha}");
		}
	}
}