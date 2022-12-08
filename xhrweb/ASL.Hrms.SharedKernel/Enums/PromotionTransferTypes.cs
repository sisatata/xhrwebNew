using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ASL.Hrms.SharedKernel.Enums
{
    public enum PromotionTransferTypes
    {
        // incrementtypeid
        [Description("Increment")]
        Increment = 10,
        [Description("Adjustment")]
        Adjustment = 20,
        [Description("Promotion")]
        Promotion = 30,
        [Description("Transfer")]
        Transfer = 40,
        [Description("Promotion & Increment")]
        PromotionAndIncrement = 50,

        [Description("Starting Salary")]
        StartingSalary = 60,
        [Description("Revised Salary")]
        RevisedSalary = 70,
        [Description("Others")]
        Others = 80

    }
}
