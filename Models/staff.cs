using System;
using System.Collections.Generic;

namespace ThetaECommerceApp.Models
{
    public partial class staff
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public DateTime? Dob { get; set; }
        public int? SystemUserId { get; set; }
        public int? Role { get; set; }
        public int? Status { get; set; }
        public string? CeatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? MetaData { get; set; }
    }
}
