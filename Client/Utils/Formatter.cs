using System.Text;

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

	public static string FormatDateTimeRelatively(DateTime dateTime, int precision = 1) {
		var span = dateTime - DateTime.Now;
		string suffix = span < TimeSpan.Zero ? "前" : "后";
		span = span.Duration();
		var components = new List<string>();
		components.Add($"{span.Days}天");
		components.Add($"{span.Hours}小时");
		components.Add($"{span.Minutes}分钟");
		components.Add($"{span.Seconds}秒");
		components.Add($"{span.Milliseconds}毫秒");
		int idx = components.FindIndex(c => !c.StartsWith("0"));
		return string.Join(null, components.Skip(idx).Take(precision + 1)) + suffix;
	}
}