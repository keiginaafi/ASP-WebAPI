using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmptyASP.ViewModels
{
    public class SupplierVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Supplier Name")]
        public string Name { get; set; }
    }
}