using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Applications.Interfaces
{
    public interface ISupplierApplication
    {
        List<Supplier> Get();
        Supplier Get(int id);
        bool Insert(SupplierVM supplierVM);
        bool Update(int id, SupplierVM supplierVM);
        bool Delete(int id);
    }
}
