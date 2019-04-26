using Core.Base;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Supplier : BaseModel
    {
        public string Name { get; set; }
        public Supplier() { }
        public Supplier(SupplierVM supplierVM)
        {
            this.Name = supplierVM.Name;
            this.CreateDate = DateTimeOffset.Now.LocalDateTime;
        }

        public virtual void Update(SupplierVM supplierVM)
        {
            this.Name = supplierVM.Name;
            this.UpdateDate = DateTimeOffset.Now.LocalDateTime;
        }

        public virtual void Delete()
        {            
            this.DeleteDate = DateTimeOffset.Now.LocalDateTime;
            this.IsDelete = true;
        }
    }
}
