using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quotationService.DataObjects
{
    public class Writer : EntityData
    {
        public int PkWriterId { get; set; }
        public string WriterName { get; set; }
        public string Content { get; set; }
        public string FkCategoryId { get; set; }
        public bool IsDaily { get; set; }
    }
}