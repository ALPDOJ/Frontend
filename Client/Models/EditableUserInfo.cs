using Client.Api;

namespace Client.Models;

public class EditableUserInfo {
	public EditableUserInfo() { }

	public EditableUserInfo(User user) {
		Username = user.Username;
		Email = user.Email;
	}

	public string? Username { get; set; }

	public string? Password { get; set; }

	public string? Email { get; set; }
}