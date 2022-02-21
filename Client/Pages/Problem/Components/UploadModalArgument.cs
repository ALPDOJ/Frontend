namespace Client.Pages.Problem;

public class UploadModalArgument {
	public bool Multiple { get; set; }

	public string Accept { get; set; }

	public long? MaxFileSize { get; set; }

	public long? TotalFileSize { get; set; }
}