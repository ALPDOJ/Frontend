using Newtonsoft.Json;

namespace Client.Api
{
    partial class ApiClient
    {
        partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings)
        {
            settings.DateFormatString = "yyyy-MM-ddTHH:mm:ss";
        }
    }
}
