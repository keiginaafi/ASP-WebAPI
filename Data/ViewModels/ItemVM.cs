using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class ItemVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Item Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Item Price")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Please Enter Item Stock")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "Please Enter Item's Supplier")]
        public int Suppliers_Id { get; set; }
    }
}
