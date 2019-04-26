using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Persistence.Interfaces
{
    public interface IItemPersistence
    {
        List<Item> Get();
        Item Get(int id);
        bool Insert(ItemVM itemVM);
        bool Update(int id, ItemVM itemVM);
        bool Delete(int id);
    }
}
