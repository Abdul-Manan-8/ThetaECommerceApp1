using System;
using System.Collections.Generic;

namespace ThetaECommerceApp.Models
{
    public partial class Seller
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? CompanyName { get; set; }
        public string? WebsiteUrl { get; set; }
        public int? Cnic { get; set; }
        public string? City { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime? Dob { get; set; }
        public int? SystemUserId { get; set; }
        public int? Type { get; set; }
        public int? MartialStatus { get; set; }
        public int? Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? MetaData { get; set; }
        public string? SeoData { get; set; }
    }
}
