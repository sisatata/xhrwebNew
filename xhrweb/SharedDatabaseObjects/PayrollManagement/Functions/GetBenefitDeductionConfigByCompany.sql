CREATE OR REPLACE FUNCTION payroll.getbenefitdeductionconfigbycompany(companyid uuid)
 RETURNS TABLE("Id" uuid, "BenefitDeductionCode" character varying, "Name" character varying, "Description" character varying, "Type" character varying, "BasicOrGross" character varying, "CalculationType" character varying, "PercentOfBasicOrGross" numeric, "FixedAmount" numeric, "IntervalId" integer, "CompanyId" uuid, "IsCalculateSalary" boolean, "IsActive" boolean, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "IsDeleted" boolean)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			BenefitDeductionConfig."Id" , 
			BenefitDeductionConfig."BenefitDeductionCode" , 
			BenefitDeductionConfig."Name" , 
			BenefitDeductionConfig."Description" , 
			BenefitDeductionConfig."Type" , 
			BenefitDeductionConfig."BasicOrGross" , 
			BenefitDeductionConfig."CalculationType" , 
			BenefitDeductionConfig."PercentOfBasicOrGross" , 
			BenefitDeductionConfig."FixedAmount" , 
			BenefitDeductionConfig."IntervalId" , 
			BenefitDeductionConfig."CompanyId" , 
			BenefitDeductionConfig."IsCalculateSalary" , 
			BenefitDeductionConfig."IsActive" , 
			BenefitDeductionConfig."StartDate" , 
			BenefitDeductionConfig."EndDate" , 
			BenefitDeductionConfig."IsDeleted" 
		 FROM payroll."BenefitDeductionConfigs" AS BenefitDeductionConfig 
		 WHERE BenefitDeductionConfig."IsDeleted" = False and BenefitDeductionConfig."CompanyId" = companyId ;
	END;
$function$
;
