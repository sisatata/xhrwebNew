CREATE OR REPLACE FUNCTION payroll.EmployeeBonusProcessedDataById(id uuid)
RETURNS TABLE (
		"Id" uuid,
		"EmployeeId" uuid ,
		"BonusTypeID" uuid ,
		"BonusDate" timestamp ,
		"FinancialYear" uuid ,
		"PaymentOptionId" int2 ,
		"CompanyId" uuid ,
		"BranchId" uuid ,
		"DepartmentId" uuid ,
		"PositionID" uuid ,
		"JoiningDate" timestamp ,
		"GradeID" uuid ,
		"Basic" numeric ,
		"HouseRent" numeric ,
		"Medical" numeric ,
		"Conveyance" numeric ,
		"Food" numeric ,
		"GrossSalary" numeric ,
		"PayableBonus" numeric ,
		"TaxDeductedAmount" numeric ,
		"NetPayableBonus" numeric ,
		"Basic_V2" numeric ,
		"HouseRent_V2" numeric ,
		"GrossSalary_V2" numeric ,
		"PayableBonus_V2" numeric ,
		"TaxDeductedAmount_V2" numeric ,
		"NetPayableBonus_V2" numeric ,
		"Remarks" character varying(500) ,
		"IsApproved" boolean ,
		"ApprovedBy" uuid ,
		"ApprovedTime" timestamp ,
		"IsDeleted" boolean ,
		"BonusConfigurationId" uuid ,
		"EmployeeName" character varying,
		"BonusYear" character varying,
		"BonusType" character varying
		)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT ebpd ."Id" ,
			ebpd."EmployeeId" , 
			ebpd."BonusTypeId" , 
			ebpd."BonusDate" , 
			ebpd."FinancialYearId" , 
			ebpd."PaymentOptionId" , 
			ebpd."CompanyId" , 
			ebpd."BranchId" , 
			ebpd."DepartmentId" , 
			ebpd."PositionId"  , 
			ee."JoiningDate" , 
			ebpd."GradeId" , 
			ebpd."Basic" , 
			ebpd."HouseRent" , 
			ebpd."Medical" , 
			ebpd."Conveyance" , 
			ebpd."Food" , 
			ebpd."GrossSalary" , 
			ebpd."PayableBonus" , 
			ebpd."TaxDeductedAmount" , 
			ebpd."NetPayableBonus" , 
			ebpd."Basic_V2" , 
			ebpd."HouseRent_V2" , 
			ebpd."GrossSalary_V2" , 
			ebpd."PayableBonus_V2" , 
			ebpd."TaxDeductedAmount_V2" , 
			ebpd."NetPayableBonus_V2" , 
			ebpd."Remarks" , 
			ebpd."IsApproved" , 
			ebpd."ApprovedBy" , 
			ebpd."ApprovedTime" , 
			ebpd."IsDeleted" , 
			ebpd."BonusConfigurationId",
			e."FullName" as "EmployeeName",
			fy."FinancialYearName"  as "BonusYear",
			clut."LookUpTypeName" as "BonusType"
		 FROM payroll."EmployeeBonusProcessedDatas" AS ebpd 
		 inner join employee."Employees" e on ebpd."EmployeeId" = e."Id" 
		 inner join employee."EmployeeEnrollments" ee on e."Id" = ee."EmployeeId" 
		 inner join main."FinancialYears" fy on ebpd."FinancialYearId" = fy."Id" 
		 left join main."CommonLookUpTypes" clut on ebpd."BonusTypeId" = clut ."Id" 
		 WHERE ebpd."IsDeleted" = False and ebpd."Id" = id ;
		END;
$BODY$
LANGUAGE plpgsql;