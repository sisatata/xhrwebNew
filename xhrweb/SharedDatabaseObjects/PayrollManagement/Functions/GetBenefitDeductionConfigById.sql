CREATE OR REPLACE FUNCTION payroll.GetBenefitDeductionConfigById(id uuid)
RETURNS TABLE (
		"Id" uuid ,
		"BenefitDeductionCode" character varying(50) ,
		"Name" character varying(50) ,
		"Description" character varying(250) ,
		"Type" character varying(15) ,
		"BasicOrGross" character varying(15) ,
		"CalculationType" character varying(15) ,
		"PercentOfBasicOrGross" Decimal ,
		"FixedAmount" Decimal ,
		"IntervalId" Int ,
		"CompanyId" uuid ,
		"IsCalculateSalary" boolean ,
		"IsActive" boolean ,
		"StartDate" timestamp(6)  ,
		"EndDate" timestamp(6)  ,
		"IsDeleted" boolean 
)
AS $BODY$
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
		 WHERE BenefitDeductionConfig."IsDeleted" = False and BenefitDeductionConfig."Id" = id ;
	END;
$BODY$
LANGUAGE plpgsql;

