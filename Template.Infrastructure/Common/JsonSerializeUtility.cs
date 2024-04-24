using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Template.Infrastructure.Common
{
    public static class JsonSerializeUtility
    {
        private static readonly JsonSerializerSettings _camelCaseSerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        };

        public static string SerializeWithCamelCase(object value)
        {
            return JsonConvert.SerializeObject(value, _camelCaseSerializerSettings);
        }

        public static T? DeserializeWithCamelCase<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json, _camelCaseSerializerSettings);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
