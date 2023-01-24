using System;
using System.Collections.Generic;

namespace ThetaECommerceApp.Models
{
    public partial class SystemUser
    {
        public SystemUser()
        {
            Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int? Type { get; set; }
        public int? Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? MetaData { get; set; }
        public string? RecoveryCode { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
