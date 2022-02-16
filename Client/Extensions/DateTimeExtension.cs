namespace Client.Extensions;

public static class DateTimeExtension {
	private const int Second = 1;

	private const int Minute = 60 * Second;

	private const int Hour = 60 * Minute;

	private const int Day = 24 * Hour;

	private const double Month = 30.4375 * Day;

	private const double Year = 12 * Month;

	public static string ToFriendlyDisplay(this DateTime dateTime) {
		var ts = DateTime.Now - dateTime;
		double delta = ts.TotalSeconds;
		string suffix = delta < 0 ? "后" : "前";
		return Math.Abs(delta) switch {
				< Minute => $"{ts.Seconds}秒",
				< Hour   => $"{ts.Minutes}分钟",
				< Day    => $"{ts.Hours}小时",
				< Month  => $"{ts.Days}天",
				< Year   => $"{(int)Math.Floor(Math.Abs(delta) / Month)}月",
				_        => $"{(int)Math.Floor(Math.Abs(delta) / Year)}年"
			} +
			suffix;
	}
}