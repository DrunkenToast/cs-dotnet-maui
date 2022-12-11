using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using cs_dotnet_maui.Exceptions;

namespace cs_dotnet_maui
{
    internal class ApiDataStore : IDataStore
    {
        public List<Item> ItemList { get; set; }
        private readonly float _price_key = 2.49F;

        public async Task DeleteItemAsync(Item it)
        {
            HttpClient client = new();
            var res = await client.DeleteAsync(Environment.baseUrl + "items/" + it.Id);

            res.EnsureSuccessStatusCode();

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

            if (res.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new NoKeysException("Not enough keys to unbox this crate");
            }

            res.EnsureSuccessStatusCode();

            var json = await res.Content.ReadAsStringAsync();

            Console.WriteLine(json);

            return _convertFromJson<Item>(json);
        }

        public async Task<int> GetKeysAmount()
        {
            HttpClient client = new();

            var res = await client.GetAsync(Environment.baseUrl + "keys");

            res.EnsureSuccessStatusCode();

            var json = await res.Content.ReadAsStringAsync();
            var keys = _convertFromJson<Keys>(json);

            return keys.Amount;
        }

        public async Task<int> PurchaseKey(int amount)
        {
            HttpClient client = new();
            var money = amount * _price_key;
            var data = $"{{'money': {amount}}}";
            Console.WriteLine($"Data sent: {data}");
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            var res = await client.PostAsync(Environment.baseUrl + "keys", content);
            res.EnsureSuccessStatusCode();
            var json = await res.Content.ReadAsStringAsync();
            var keys = _convertFromJson<Keys>(json);

            return keys.Amount;
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
