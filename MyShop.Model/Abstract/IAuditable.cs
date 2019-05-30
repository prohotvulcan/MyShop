using System;

namespace MyShop.Data.Abstract
{
    public interface IAuditable
    {
        DateTime? CreatedDate { set; get; }
        DateTime? UpdateDate { set; get; }
        string CreatedBy { set; get; }
        string UpdateBy { get; set; }
        bool Status { set; get; }
    }
}
