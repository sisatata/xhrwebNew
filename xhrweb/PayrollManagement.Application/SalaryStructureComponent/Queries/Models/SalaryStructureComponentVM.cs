using ASL.Hrms.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.SalaryStructureComponent.Queries.Models
{
    public class SalaryStructureComponentVM
    {
        private string _valueType;
        private string _percentOn;
        public Guid? Id { get; set; }
        public Guid? SalaryStrutureId { get; set; }
        public string SalaryComponentName { get; set; }
        public decimal? Value { get; set; }
        public string ValueTypeText { get; set; }
        //public string ValueType { get; set; }
        public string ValueType
        {
            get { return this._valueType; }
            set
            {
                this._valueType = value;
                ValueTypeText = ((ValueTypes)Convert.ToInt32(!string.IsNullOrEmpty(this._valueType) ? this._valueType : "1")).ToString();
            }
        }
        public string PercentOnText { get; set; }
        //public string PercentOn { get; set; }

        public string PercentOn
        {
            get { return this._percentOn; }
            set
            {
                this._percentOn = value;
                if (!string.IsNullOrEmpty(this._percentOn))
                    PercentOnText = ((PercentOnTypes)Convert.ToInt32(this._percentOn)).ToString();
                //PercentOnText = ((PercentOnTypes)Convert.ToInt32(!string.IsNullOrEmpty(this._percentOn) ? this._percentOn : "1")).ToString();
            }
        }
        public Boolean IsDeleted { get; set; }
        public Guid? CompanyId { get; set; }
        public short SortOrder { get; set; }
    }
}
