using System;
using System.Collections.Generic;

namespace ThetaECommerceApp.Models
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public string? CustomerId { get; set; }
        public string? OrderId { get; set; }
        public string? Feedback1 { get; set; }
        public string? Image { get; set; }
        public string? Status { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? ModifiedDate { get; set; }
    }
}
