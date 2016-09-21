using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quotationService.DataObjects
{
    public class Category : EntityData
    {
        public int PkCategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsDaily { get; set; }
        public string WriterName { get; set; }
    }
}