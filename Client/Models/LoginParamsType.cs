using System.ComponentModel.DataAnnotations;

namespace Client.Models; 

public class LoginParamsType {
	[Required]
	public int Id { get; set; }

	[Required]
	public string Password { get; set; }

	public bool AutoLogin { get; set; }
}