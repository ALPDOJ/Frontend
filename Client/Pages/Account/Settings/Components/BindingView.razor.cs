using Client.Models;

namespace Client.Pages.Account.Settings
{
	public partial class BindingView
	{
		private readonly UserLiteItem[] _data = {
		new() {
			Avater = "taobao",
			Title = "Binding Taobao",
			Description = "Currently unbound Taobao account"
		},
		new() {
			Avater = "alipay",
			Title = "Binding Alipay",
			Description = "Currently unbound Alipay account"
		},
		new() {
			Avater = "dingding",
			Title = "Binding DingTalk",
			Description = "Currently unbound DingTalk account"
		}
	};
	}
}