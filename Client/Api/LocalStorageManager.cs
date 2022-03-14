using Blazored.LocalStorage;

namespace Client.Api;

public class LocalStorageManager {
	public LocalStorageManager(ISyncLocalStorageService localStorage, ILocalStorageService asyncLocalStorage, ApiClient api) {
		LocalStorage = localStorage;
		AsyncLocalStorage = asyncLocalStorage;
		Api = api;
	}

	public int? Id {
		get => this["id"] is { } id ? int.Parse(id) : null;
		set => this["id"] = value?.ToString();
	}

	public string? Password {
		get => this["password"];
		set => this["password"] = value;
	}

	public User? CurrentUser {
		get => Get<User?>("currentUser");
		set => Set("currentUser", value);
	}

	public string? CurrentUserAvatar {
		get => this["currentUserAvatar"];
		set => this["currentUserAvatar"] = value;
	}

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

	public async Task<User> RefreshCurrentUser() => CurrentUser = (await Api.GetCurrentUserAsync()).Result;

	public async Task<string?> RefreshCurrentUserAvatar() {
		int id = (await GetCurrentUser()).Id;
		var response = await Api.GetUserAvatarAsync(id);
		string avatar = Convert.ToBase64String(await response.GetByteArray());
		return CurrentUserAvatar = string.IsNullOrEmpty(avatar)
			? null
			: $"data:{response.Headers["Content-Type"].Single()};base64,{avatar}";
	}

	public async Task Refresh() {
		await RefreshCurrentUser();
		await RefreshCurrentUserAvatar();
	}

	public async Task<User> GetCurrentUser(bool force = false) {
		if (force)
			return await RefreshCurrentUser();
		return CurrentUser ?? await RefreshCurrentUser();
	}

	public async Task<string?> GetCurrentUserAvatar(bool force = false) {
		if (force)
			return await RefreshCurrentUserAvatar();
		return CurrentUserAvatar ?? await RefreshCurrentUserAvatar();
	}
}