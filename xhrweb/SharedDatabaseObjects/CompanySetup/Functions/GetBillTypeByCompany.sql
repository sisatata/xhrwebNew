CREATE OR REPLACE FUNCTION main.getbilltypebycompany(companyid uuid, employeeid uuid)
 RETURNS TABLE("BenefitDeductionId" uuid, "BillTypeName" character varying, "CompanyId" uuid, allocatedamount numeric)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			BenefitDeductionConfig."Id" as BenefitDeductionId, 
			BenefitDeductionConfig."Name" as BillTypeName ,
			BenefitDeductionConfig."CompanyId",
			cast(COALESCE (ea."Amount" ,0) as numeric) as AllocatedAmount
			FROM payroll."BenefitDeductionConfigs" AS BenefitDeductionConfig 
			left join payroll."BenefitDeductionEmployeeAssigneds" ea on ea."BenefitDeductionId" = BenefitDeductionConfig."Id" 
			and ea."EmployeeId" = employeeId
		 WHERE BenefitDeductionConfig."IsDeleted" = False and BenefitDeductionConfig."CompanyId" = companyid 
		and date(BenefitDeductionConfig."StartDate") <= current_date and  date(BenefitDeductionConfig."EndDate") >=current_date
	and UPPER(BenefitDeductionConfig."Type") ='BENEFIT' and BenefitDeductionConfig."IsCalculateSalary" = false;
	END;
$function$
;
