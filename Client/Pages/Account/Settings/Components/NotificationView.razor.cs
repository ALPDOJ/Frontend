using Client.Models;

namespace Client.Pages.Account.Settings; 

public partial class NotificationView {
	private readonly UserLiteItem[] _data = {
		new() {
			Title = "Account Password",
			Description = "Messages from other users will be notified in the form of a station letter"
		},
		new() {
			Title = "System Messages",
			Description = "System messages will be notified in the form of a station letter"
		},
		new() {
			Title = "To-do Notification",
			Description = "The to-do list will be notified in the form of a letter from the station"
		}
	};
}