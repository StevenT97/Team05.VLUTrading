namespace SEP_Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    public partial class Product
    {
        public HttpPostedFileBase Image { get; set; }
        public IEnumerable<HttpPostedFileBase> Image_Detail { get; set; }
    }
}
