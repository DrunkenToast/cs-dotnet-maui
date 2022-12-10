using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace cs_dotnet_maui
{
    internal class ApiDataStore : IDataStore
    {
        public List<Item> ItemList { get; set; }

        public async Task DeleteItemAsync(Item it) //TODO: test
        {
            HttpClient client = new();
            await client.DeleteAsync(Environment.baseUrl + "items/" + it.Id);

            return;
        }

        public async Task<List<Item>> GetAllItemsAsync()
        {
            HttpClient client = new();
            String json = await client.GetStringAsync(Environment.baseUrl + "items");

            Console.WriteLine(json);

            return _convertFromJson<List<Item>>(json);
        }

        public async Task<Item> UnboxItemAsync() //TODO: test
        {
            HttpClient client = new();
            var res = await client.PostAsync(Environment.baseUrl + "items", null);
            var json = await res.Content.ReadAsStringAsync();

            Console.WriteLine(json);

            return _convertFromJson<Item>(json);
        }

        private T _convertFromJson<T>(string json)
        {
            var opt = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(json, opt);
        }
    }
}
