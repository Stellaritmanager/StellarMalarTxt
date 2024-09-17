using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StellarBillingSystem.Models
{
    public class ProductDropDownModel
    {
        public ProductDropDownModel() { }

        private String selectedItemId;
        private IEnumerable<SelectListItem> items;
        private ProductMatserModel objPro;

      
       
        public ProductMatserModel ObjPro { get => objPro; set => objPro = value; }
        public string? SelectedItemId { get => selectedItemId; set => selectedItemId = value; }
        public IEnumerable<SelectListItem> Items { get => items; set => items = value; }
    }
}
