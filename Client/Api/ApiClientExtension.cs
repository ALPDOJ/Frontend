using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using JsonException = System.Text.Json.JsonException;

namespace Client.Api;

public static class ApiClientExtension {
	private static Type ApiClientType { get; } = typeof(ApiClient);

	private static FieldInfo ApiClientHttpClientField { get; } = ApiClientType.GetField("_httpClient", BindingFlags.Instance | BindingFlags.NonPublic)!;

	private static HttpClient GetHttpClient(this ApiClient apiClient) => (HttpClient)ApiClientHttpClientField.GetValue(apiClient)!;

	public static async Task<byte[]> GetByteArray(this FileResponse fileResponse) {
		var stream = new MemoryStream();
		await fileResponse.Stream.CopyToAsync(stream);
		return stream.ToArray();
	}

	public static async Task<ApiResponse<IdWrapper>> SubmitAsync(this ApiClient api, NewSubmission submission) {
		var client = api.GetHttpClient();
		var content = new MultipartFormDataContent {
			{ new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(submission.Test ? "true" : "false"))), "test" },
			{ new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(submission.ProblemId.ToString()))), "problemId" }
		};
		foreach (var (fileName, fileContent) in submission.Files) {
			var streamContent = new StreamContent(new MemoryStream(fileContent));
			content.Add(streamContent, "files", fileName);
		}
		var response = await client.PostAsync($"{api.BaseUrl}/submission", content);
		var status = (int)response.StatusCode;
		var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
		switch (response.StatusCode) {
			case HttpStatusCode.Created:
				try {
					var body = await response.Content.ReadFromJsonAsync<IdWrapper>();
					if (body is null)
						throw new ApiException("Response was null which was not expected.", status, await response.Content.ReadAsStringAsync(), headers, null);
					return new ApiResponse<IdWrapper>(status, headers, body);
				}
				catch (JsonException ex) {
					throw new ApiException($"Could not deserialize the response body string as {typeof(IdWrapper).FullName}.", status, await response.Content.ReadAsStringAsync(), headers, ex);
				}
			case HttpStatusCode.BadRequest: throw new ApiException("请求错误，参数或负载非法", status, await response.Content.ReadAsStringAsync(), headers, null);
			default:                        throw new ApiException($"The HTTP status code of the response was not expected ({status}).", status, await response.Content.ReadAsStringAsync(), headers, null);
		}
	}
}