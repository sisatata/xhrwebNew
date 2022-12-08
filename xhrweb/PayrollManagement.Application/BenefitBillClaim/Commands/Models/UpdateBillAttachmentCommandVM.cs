using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.BenefitBillClaim.Commands.Models
{
  public  class UpdateBillAttachmentCommandVM
    {
        public Guid Id { get; set; }
        public string PictureUri { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
