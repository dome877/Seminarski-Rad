using Newtonsoft.Json;

namespace Seminarski_Rad.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObjectAsJson
            (this ISession session, string key, object value)
        {
            
            var serializedString = JsonConvert.SerializeObject(value);
            session.SetString(key, serializedString);
        }

        public static T GetObjectAsJson<T>(this ISession session, string key)
        {
            
            var value = session.GetString(key);

            if (value == null) return default(T);

            return JsonConvert.DeserializeObject<T>(value);

        }
    }
}
