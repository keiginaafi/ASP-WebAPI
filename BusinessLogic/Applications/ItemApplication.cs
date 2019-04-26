using BusinessLogic.Applications.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Data.ViewModels;
using Common.Persistence.Interfaces;

namespace BusinessLogic.Applications
{
    public class ItemApplication : IItemApplication
    {
        private readonly IItemPersistence _itemPersistence;
        public ItemApplication(IItemPersistence itemPersistence)
        {
            _itemPersistence = itemPersistence;
        }

        public bool Delete(int id)
        {
            if (string.IsNullOrEmpty(id.ToString()) || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return false;
            }
            else
            {
                return _itemPersistence.Delete(id);
            }
        }

        public List<Item> Get()
        {
            return _itemPersistence.Get();
        }

        public Item Get(int id)
        {
            return _itemPersistence.Get(id);
        }

        public bool Insert(ItemVM itemVM)
        {
            //int price, stock, supplier_id;
            if (string.IsNullOrEmpty(itemVM.Name) || string.IsNullOrWhiteSpace(itemVM.Name))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(itemVM.Price.ToString()) || string.IsNullOrWhiteSpace(itemVM.Price.ToString()) || itemVM.Price < 0)
            {
                return false;
            }
            else if (string.IsNullOrEmpty(itemVM.Stock.ToString()) || string.IsNullOrWhiteSpace(itemVM.Stock.ToString()) || itemVM.Stock < 0)
            {
                return false;
            }
            else if (string.IsNullOrEmpty(itemVM.Suppliers_Id.ToString()) || string.IsNullOrWhiteSpace(itemVM.Suppliers_Id.ToString()))
            {
                return false;
            }
            else
            {
                return _itemPersistence.Insert(itemVM);
            }
        }

        public bool Update(int id, ItemVM itemVM)
        {
            if (string.IsNullOrEmpty(id.ToString()) || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(itemVM.Name) || string.IsNullOrWhiteSpace(itemVM.Name))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(itemVM.Price.ToString()) || string.IsNullOrWhiteSpace(itemVM.Price.ToString()) || itemVM.Price < 0)
            {
                return false;
            }
            else if (string.IsNullOrEmpty(itemVM.Stock.ToString()) || string.IsNullOrWhiteSpace(itemVM.Stock.ToString()) || itemVM.Stock < 0)
            {
                return false;
            }
            else if (string.IsNullOrEmpty(itemVM.Suppliers_Id.ToString()) || string.IsNullOrWhiteSpace(itemVM.Suppliers_Id.ToString()))
            {
                return false;
            }
            else
            {
                return _itemPersistence.Update(id, itemVM);
            }
        }
    }
}
