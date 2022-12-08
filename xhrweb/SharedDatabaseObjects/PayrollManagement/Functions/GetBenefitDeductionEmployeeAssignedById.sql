CREATE OR REPLACE FUNCTION payroll.GetBenefitDeductionEmployeeAssignedById(id uuid )
RETURNS TABLE (
		"Id" uuid ,
		"BenefitDeductionId" uuid ,
		"EmployeeId" uuid ,
		"Remarks" character varying(250) ,
		"IsDeleted" boolean ,
		"StartDate" timestamp(6) ,
		"EndDate"  timestamp(6) ,
		"Amount" Decimal
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			BenefitDeductionEmployeeAssigned."Id" , 
			BenefitDeductionEmployeeAssigned."BenefitDeductionId" , 
			BenefitDeductionEmployeeAssigned."EmployeeId" , 
			BenefitDeductionEmployeeAssigned."Remarks" , 
			BenefitDeductionEmployeeAssigned."IsDeleted" , 
			BenefitDeductionEmployeeAssigned."StartDate" , 
			BenefitDeductionEmployeeAssigned."EndDate" , 
			BenefitDeductionEmployeeAssigned."Amount" 
		 FROM payroll."BenefitDeductionEmployeeAssigneds" AS BenefitDeductionEmployeeAssigned 
		 WHERE BenefitDeductionEmployeeAssigned."IsDeleted" = False and BenefitDeductionEmployeeAssigned."Id" = id;
	END;
$BODY$
LANGUAGE plpgsql;

