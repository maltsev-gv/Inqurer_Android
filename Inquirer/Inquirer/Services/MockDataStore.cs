using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InquirerForAndroid.Models;

namespace InquirerForAndroid.Services
{
    public class MockDataStore //: IDataStore<ItemInfo>
    {
        readonly List<ItemInfo> items;

        public MockDataStore()
        {
            items = new List<ItemInfo>()
            {
                new ItemInfo { Id = Guid.NewGuid().ToString(), Text = "Место работы", ItemType = ItemTypes.Header },
                new ItemInfo { Id = Guid.NewGuid().ToString(), Text = "Предприятие", ValueList=new List<string>() { "РУК", "НМТК"}, ValueType = ValueTypes.List },
                new ItemInfo { Id = Guid.NewGuid().ToString(), Text = "Оцените качество социально-бытовых условий", ItemType = ItemTypes.Header  },
                new ItemInfo { Id = Guid.NewGuid().ToString(), Text = "Раздевалка \"чистая\"", ItemType = ItemTypes.SubHeader },
                new ItemInfo { Id = Guid.NewGuid().ToString(), Text = "Шкафы", ValueType = ValueTypes.Stars},
                new ItemInfo { Id = Guid.NewGuid().ToString(), Text = "Лавки", ValueType = ValueTypes.Stars},
            };
        }

        public async Task<bool> AddItemAsync(ItemInfo item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ItemInfo item)
        {
            var oldItem = items.Where((ItemInfo arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((ItemInfo arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ItemInfo> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ItemInfo>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}