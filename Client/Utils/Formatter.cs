namespace Client.Utils;

public static class Formatter {
	public static string FormatSize(long size, int precision = 1) {
		string format = precision == 0 ? "##" : "##." + new string('#', precision);
		return size switch {
			< 1 << 10 => $"{size} B",
			< 1 << 20 => $"{((double)size / (1 << 10)).ToString(format)} KB",
			< 1 << 30 => $"{((double)size / (1 << 20)).ToString(format)} MB",
			_         => $"{((double)size / (1 << 30)).ToString(format)} GB"
		};
	}
}