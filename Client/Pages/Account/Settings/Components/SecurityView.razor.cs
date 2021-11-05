using Client.Models;

namespace Client.Pages.Account.Settings; 

public partial class SecurityView {
	private readonly UserLiteItem[] _data = {
		new() {
			Title = "Account Password",
			Description = "Current password strength: : Strong"
		},
		new() {
			Title = "Security Phone",
			Description = "Bound phone: : 138****8293"
		},
		new() {
			Title = "Security Question",
			Description =
				"The security question is not set, and the security policy can effectively protect the account security"
		},
		new() {
			Title = "Backup Email",
			Description = "Bound Email: : ant***sign.com"
		},
		new() {
			Title = "MFA Device",
			Description = "Unbound MFA device, after binding, can be confirmed twice"
		}
	};
}