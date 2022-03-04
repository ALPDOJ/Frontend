using System.Text;
using System.Text.RegularExpressions;

namespace Client.Api;

public class ApiClientBase {
	public static string BaseUrl { get; set; }

	public static IList<ServerUrl> Servers { get; } = new List<ServerUrl>();

	protected HttpClient HttpClient { get; }

	public ApiClientBase() : this(new HttpClient()) { }

	public ApiClientBase(HttpClient httpClient) => HttpClient = httpClient;
}

public class ServerUrl {
	private static Regex TemplatePattern { get; } = new(@"\{(?<name>\w+)\}", RegexOptions.Compiled);

	private readonly IList<Match>? _templates;

	public string UrlTemplate { get; }

	public IDictionary<string, Variable>? Variables { get; }

	public ServerUrl(string url) : this(url, null) { }

	public ServerUrl(string urlTemplate, IDictionary<string, Variable>? variables) {
		UrlTemplate = urlTemplate;
		Variables = variables;
		if (variables is not null) {
			_templates = TemplatePattern.Matches(urlTemplate);
			foreach (var match in _templates) {
				string name = match.Groups["name"].Value;
				if (!variables.ContainsKey(name))
					throw new ArgumentException($"Variable {name} not found");
			}
		}
	}

	public override string ToString() {
		if (_templates is null)
			return UrlTemplate;
		var builder = new StringBuilder();
		if (_templates[0].Index > 0)
			builder.Append(UrlTemplate[.._templates[0].Index]);
		builder.Append(Variables![_templates[0].Groups["name"].Value]);
		for (var i = 1; i < _templates.Count; ++i) {
			var match = _templates[i];
			var prev = _templates[i - 1];
			if (prev.Index + prev.Length < match.Index)
				builder.Append(UrlTemplate[(prev.Index + prev.Length)..match.Index]);
			builder.Append(Variables![match.Groups["name"].Value]);
		}
		var last = _templates[^1];
		if (last.Index + last.Length < UrlTemplate.Length)
			builder.Append(UrlTemplate[(last.Index + last.Length)..]);
		return builder.ToString();
	}

	public static implicit operator string(ServerUrl serverUrl) => serverUrl.ToString();

	public class Variable {
		private string _value;

		public string Default { get; }

		public string[] Enum { get; }

		public string Value {
			get => _value;
			set {
				if (!Enum.Contains(value))
					throw new ArgumentException($"Value {value} doesn't exist in enum");
				_value = value;
			}
		}

		public Variable(string @default) : this(@default, Array.Empty<string>()) { }

		public Variable(string @default, IEnumerable<string> @enum) {
			Value = Default = @default;
			Enum = @enum.ToArray();
		}

		public override string ToString() => Value;

		public static implicit operator string(Variable v) => v.Value;
	}
}