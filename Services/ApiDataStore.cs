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

        public Task DeleteItemAsync(Item it)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Item>> GetAllItemsAsync()
        {
            HttpClient client = new();
            String json = await client.GetStringAsync(Environment.baseUrl + "items");

            Console.WriteLine(json);

            var opt = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            };

            ItemList = JsonSerializer.Deserialize<List<Item>>(json, opt);

            return ItemList;
        }

        public Task<Item> UnboxItemAsync()
        {
            throw new NotImplementedException();
        }
    }
}
