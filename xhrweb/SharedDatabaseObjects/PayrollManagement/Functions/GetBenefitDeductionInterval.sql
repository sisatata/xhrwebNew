CREATE OR REPLACE FUNCTION payroll.GetBenefitDeductionInterval()
RETURNS TABLE (
		"Id" uuid ,
		"IntervalId" Int ,
		"IntervalName" character varying(50) 
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			BenefitDeductionInterval."Id" , 
			BenefitDeductionInterval."IntervalId" , 
			BenefitDeductionInterval."IntervalName" 
		 FROM payroll."BenefitDeductionIntervals" AS BenefitDeductionInterval 
		 WHERE BenefitDeductionInterval."IsDeleted" = False ;
	END;
$BODY$
LANGUAGE plpgsql;