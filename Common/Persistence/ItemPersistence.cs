using Common.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Data.ViewModels;
using Data;
using System.Data.Entity;

namespace Common.Persistence
{
    public class ItemPersistence : IItemPersistence
    {
        MyContext myContext = new MyContext();
        bool status = false;

        public bool Delete(int id)
        {
            var get = myContext.Items.Find(id);
            get.Delete();
            myContext.Entry(get).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            if (result > 0)
                status = true;
            return status;
        }

        public List<Item> Get()
        {
            return myContext.Items.Include("Suppliers").Where(x => x.IsDelete == false).ToList();
        }

        public Item Get(int id)
        {
            var get = myContext.Items.Find(id);
            return get;
        }

        public bool Insert(ItemVM itemVM)
        {
            var push = new Item(itemVM);
            var supplier = myContext.Suppliers.Find(itemVM.Suppliers_Id);
            push.Suppliers = supplier;
            myContext.Items.Add(push);
            var result = myContext.SaveChanges();
            if (result > 0)
                status = true;
            return status;
        }

        public bool Update(int id, ItemVM itemVM)
        {
            var get = myContext.Items.Find(id);
            get.Update(itemVM);
            var supplier = myContext.Suppliers.Find(itemVM.Suppliers_Id);
            get.Suppliers = supplier;
            myContext.Entry(get).State = EntityState.Modified;
            myContext.Entry(get.Suppliers).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            if (result > 0)
                status = true;
            return status;
        }
    }
}