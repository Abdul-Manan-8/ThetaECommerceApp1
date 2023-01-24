using System;
using System.Collections.Generic;

namespace ThetaECommerceApp.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string? Remarks { get; set; }
        public decimal? OrderPrice { get; set; }
        public int? PaymentMethod { get; set; }
        public decimal? OrderDiscount { get; set; }
        public decimal? FinalPrice { get; set; }
        public int? Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? MetaData { get; set; }
    }
}
