using Client.Models;

namespace Client.Services
{
	public interface IAccountService
	{
		Task LoginAsync(LoginParamsType model);

		Task<string> GetCaptchaAsync(string modile);
	}

	public class AccountService : IAccountService
	{
		private readonly Random _random = new();

		public Task LoginAsync(LoginParamsType model)
			=>
				// todo: login logic
				Task.CompletedTask;

		public Task<string> GetCaptchaAsync(string modile)
		{
			string captcha = _random.Next(0, 9999).ToString().PadLeft(4, '0');
			return Task.FromResult(captcha);
		}
	}
}