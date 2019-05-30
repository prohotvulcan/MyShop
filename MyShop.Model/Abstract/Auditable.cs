using System;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Data.Abstract
{
    public abstract class Auditable : IAuditable
    {
        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        [MaxLength(256)]
        public string CreatedBy { get; set; }

        [MaxLength(256)]
        public string UpdateBy { get; set; }

        public bool Status { set; get; }
    }
}
