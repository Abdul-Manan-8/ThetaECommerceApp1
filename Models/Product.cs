using System;
using System.Collections.Generic;

namespace ThetaECommerceApp.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? SellerId { get; set; }
        public string? Image { get; set; }
        public int? Quantity { get; set; }
        public int? CategoryId { get; set; }
        public decimal? Price { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public int? DeliveryDays { get; set; }
        public int? DeliveryCharges { get; set; }
        public int? Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? MetaData { get; set; }
        public string? SeoData { get; set; }
        public int? NoOfView { get; set; }
    }
}
