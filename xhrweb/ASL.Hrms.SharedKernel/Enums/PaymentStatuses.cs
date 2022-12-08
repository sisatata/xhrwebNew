using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ASL.Hrms.SharedKernel.Enums
{
    public enum PaymentStatuses
    {
        [Description("Unpaid")]
        Unpaid = 1,
        [Description("Partial Paid")]
        PartPaid = 2,
        [Description("Paid")]
        Paid = 3,
        [Description("Settled")]
        Settled = 4,
        [Description("Partial Refunded")]
        PartRefunded = 5,
        [Description("Refunded")]
        Refunded = 6,
        [Description("Voided")]
        Voided = 7




    }
}
