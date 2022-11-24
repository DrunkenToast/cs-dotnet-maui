using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_dotnet_maui
{
    internal interface IDataStore
    {
        List<Item> ItemList { get; set; }
        Task<List<Item>> GetAllItemsAsync();
        Task<Item> UnboxItemAsync();
        Task DeleteItemAsync(Item it);
    }
}
