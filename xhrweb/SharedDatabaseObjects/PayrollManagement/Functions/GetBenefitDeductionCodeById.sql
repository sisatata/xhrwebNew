CREATE OR REPLACE FUNCTION payroll.GetBenefitDeductionCodeById(id uuid)
RETURNS TABLE (
		"Id" uuid ,
		"CompanyId" uuid ,
		"BenifitDeductionCode" character varying(50) ,
		"BenifitDeductionCodeName" character varying(100) 
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			BenefitDeductionCode."Id" , 
			BenefitDeductionCode."CompanyId" , 
			BenefitDeductionCode."BenifitDeductionCode" , 
			BenefitDeductionCode."BenifitDeductionCodeName" 
		 FROM payroll."BenefitDeductionCodes" AS BenefitDeductionCode 
		 WHERE BenefitDeductionCode."IsDeleted" = False and BenefitDeductionCode."Id" = id;
	END;
$BODY$
LANGUAGE plpgsql;

