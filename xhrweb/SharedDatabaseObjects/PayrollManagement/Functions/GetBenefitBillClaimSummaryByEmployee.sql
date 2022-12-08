CREATE OR REPLACE FUNCTION payroll.getbenefitbillclaimsummarybyemployee(employeeid uuid, startdate timestamp without time zone, enddate timestamp without time zone)
 RETURNS TABLE("BenefitDeductionId" uuid, "BenefitDeductionCode" character varying, "Name" character varying, "EmployeeId" uuid, "AllocatedAmount" numeric, "ClaimAmount" numeric, "ApprovedAmount" numeric)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		select
			BenefitBillClaim."BenefitDeductionId" ,
			dc."BenefitDeductionCode" ,
			dc."Name" ,
			BenefitBillClaim."EmployeeId" ,
			sum(nullif (BenefitBillClaim."AllocatedAmount",0.0)) AllocatedAmount,
			sum(nullif (BenefitBillClaim."ClaimAmount",0.0)) ClaimAmount,
			sum(nullif (BenefitBillClaim."ApprovedAmount",0.0)) ApprovedAmount
		from
			payroll."BenefitBillClaims" as BenefitBillClaim  inner join payroll."BenefitDeductionConfigs" dc on BenefitBillClaim."BenefitDeductionId" = dc."Id" 
		 WHERE BenefitBillClaim."IsDeleted" = false and BenefitBillClaim."EmployeeId"  = employeeId 
		and (startdate is null or BenefitBillClaim."BillDate" :: date >= startdate)
	and (enddate is null or BenefitBillClaim."BillDate" :: date <= enddate)
	group by
		BenefitBillClaim."BenefitDeductionId",
		BenefitBillClaim."EmployeeId",
		dc."BenefitDeductionCode",
		dc."Name";
	END;
$function$
;
