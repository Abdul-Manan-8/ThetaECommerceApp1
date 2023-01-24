using System;
using System.Collections.Generic;

namespace ThetaECommerceApp.Models
{
    public partial class Discount
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? DiscountedPrice { get; set; }
        public int? Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
