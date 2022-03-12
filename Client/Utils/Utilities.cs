namespace Client.Utils;

public static class Utilities {
	public static string ToQueryString(this Dictionary<string, string> queries)
		=> string.Join('&', queries.Select(pair => $"{Uri.EscapeDataString(pair.Key)}={Uri.EscapeDataString(pair.Value)}"));
}