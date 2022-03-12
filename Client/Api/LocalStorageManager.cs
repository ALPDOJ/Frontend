using Blazored.LocalStorage;

namespace Client.Api;

public class LocalStorageManager {
	public LocalStorageManager(ISyncLocalStorageService localStorage, ILocalStorageService asyncLocalStorage, ApiClient api) {
		LocalStorage = localStorage;
		AsyncLocalStorage = asyncLocalStorage;
		Api = api;
	}

	public User? CurrentUser => LocalStorage.GetItem<User?>("currentUser");

	public string? CurrentUserAvatar => LocalStorage.GetItemAsString("currentUserAvatar");

	private ISyncLocalStorageService LocalStorage { get; }

	private ILocalStorageService AsyncLocalStorage { get; }

	private ApiClient Api { get; }

	public string? this[string key] {
		get => LocalStorage.ContainKey(key) ? LocalStorage.GetItemAsString(key) : null;
		set {
			if (value is null)
				LocalStorage.RemoveItem(key);
			else
				LocalStorage.SetItemAsString(key, value);
		}
	}

	public bool ContainsKey(string key) => LocalStorage.ContainKey(key);

	public T Get<T>(string key) => LocalStorage.GetItem<T>(key);

	public ValueTask<T> GetAsync<T>(string key, CancellationToken? cancellationToken = null) => AsyncLocalStorage.GetItemAsync<T>(key, cancellationToken);

	public void Set<T>(string key, T value) => LocalStorage.SetItem(key, value);

	public ValueTask SetAsync<T>(string key, T value, CancellationToken? cancellationToken = null) => AsyncLocalStorage.SetItemAsync(key, value, cancellationToken);

	public void Remove(params string[] keys) => LocalStorage.RemoveItems(keys);

	public ValueTask RemoveAsync(params string[] keys) => AsyncLocalStorage.RemoveItemsAsync(keys);

	public void Clear() => LocalStorage.Clear();

	public ValueTask ClearAsync(CancellationToken? cancellationToken = null) => AsyncLocalStorage.ClearAsync(cancellationToken);

	public async Task Refresh() {
		var user = (await Api.GetCurrentUserAsync()).Result;
		var response = await Api.GetUserAvatarAsync(user.Id);
		string avatar = Convert.ToBase64String(await response.GetByteArray());
		await SetAsync("currentUser", user);
		this["currentUserAvatar"] = $"data:{response.Headers["Content-Type"].Single()};base64,{avatar}";
	}

	public async Task RefreshIfNotExisted() {
		if (!ContainsKey("currentUser") || !ContainsKey("currentUserAvatar"))
			await Refresh();
	}

	public async Task<User> GetCurrentUser() {
		if (!LocalStorage.ContainKey("currentUser"))
			await Refresh();
		return CurrentUser!;
	}

	public async Task<string?> GetCurrentUserAvatar() {
		if (!LocalStorage.ContainKey("currentUserAvatar"))
			await Refresh();
		return CurrentUserAvatar;
	}
}