using System;
using System.Collections.Generic;

namespace ThetaECommerceApp.Models
{
    public partial class OrderLine
    {
        public int Id { get; set; }
        public string? OrderId { get; set; }
        public string? DiscountPrice { get; set; }
        public string? SellerId { get; set; }
        public string? ProductId { get; set; }
        public string? Quantity { get; set; }
        public string? ExpectedDeliveryDate { get; set; }
        public string? CouriorName { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Status { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? ModifiedDate { get; set; }
        public string? MetaData { get; set; }
    }
}
