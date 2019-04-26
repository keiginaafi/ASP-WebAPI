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
    public class SupplierApplication : ISupplierApplication
    {
        private readonly ISupplierPersistence _supplierPersistence;
        public SupplierApplication(ISupplierPersistence supplierPersistence)
        {
            _supplierPersistence = supplierPersistence;
        }
        public bool Delete(int id)
        {
            if(string.IsNullOrEmpty(id.ToString()) || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return false;
            }
            else
            {
                return _supplierPersistence.Delete(id);
            }
        }

        public List<Supplier> Get()
        {
            //throw new NotImplementedException();
            return _supplierPersistence.Get();
        }

        public Supplier Get(int id)
        {
            return _supplierPersistence.Get(id);
        }

        public bool Insert(SupplierVM supplierVM)
        {
            //throw new NotImplementedException();
            if(string.IsNullOrEmpty(supplierVM.Name) || string.IsNullOrWhiteSpace(supplierVM.Name))
            {
                return false;
            }
            else
            {
                return _supplierPersistence.Insert(supplierVM);
            }
        }

        public bool Update(int id, SupplierVM supplierVM)
        {
            if (string.IsNullOrEmpty(id.ToString()) || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(supplierVM.Name) || string.IsNullOrWhiteSpace(supplierVM.Name))
            {
                return false;
            }
            else
            {
                return _supplierPersistence.Update(id, supplierVM);
            }
        }
    }
}
