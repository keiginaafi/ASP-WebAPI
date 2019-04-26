using Core.Base;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Item : BaseModel
    {        
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public virtual Supplier Suppliers { get; set; }
        public Item() { }
        public Item(ItemVM itemVM)
        {
            this.Name = itemVM.Name;
            this.Price = itemVM.Price;
            this.Stock = itemVM.Stock;
            //this.Suppliers = myContext.Suppliers.Find(itemVM.Suppliers_Id);
            this.CreateDate = DateTimeOffset.Now.LocalDateTime;
        }

        public virtual void Update(ItemVM itemVM)
        {            
            this.Name = itemVM.Name;
            this.Price = itemVM.Price;
            this.Stock = itemVM.Stock;
            //this.Suppliers = myContext.Suppliers.Find(itemVM.Suppliers_Id);
            this.UpdateDate = DateTimeOffset.Now.LocalDateTime;
        }

        public virtual void Delete()
        {
            this.DeleteDate = DateTimeOffset.Now.LocalDateTime;
            this.IsDelete = true;
        }
    }
}
