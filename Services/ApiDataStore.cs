using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using cs_dotnet_maui.Exceptions;

namespace cs_dotnet_maui
{
    internal class ApiDataStore : IDataStore
    {
        public List<Item> ItemList { get; set; }

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

            return _convertFromJson<List<Item>>(json);
        }

        public async Task<Item> UnboxItemAsync()
        {
            HttpClient client = new();
            var res = await client.PostAsync(Environment.baseUrl + "items", null);

            if (res.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new NoKeysException("Not enough keys to unbox this crate");
            }

            res.EnsureSuccessStatusCode();

            var json = await res.Content.ReadAsStringAsync();

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
            var jsonObj = new JsonObject();
            jsonObj.Add("amount", amount);
            var content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");

            var res = await client.PostAsync(Environment.baseUrl + "purchase", content);

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
