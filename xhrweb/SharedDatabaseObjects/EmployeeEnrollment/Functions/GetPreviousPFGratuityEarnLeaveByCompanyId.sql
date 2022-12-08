CREATE OR REPLACE FUNCTION employee.GetPreviousPFGratuityEarnLeaveByCompanyId(companyId uuid)
RETURNS TABLE (
		"Id" uuid ,
		"EmployeeId" uuid ,
		"PFAmount" numeric ,
		"GratuityAmount" numeric ,
		"EarnLeaveAmount" numeric ,
		"TillDate" timestamp ,
		"CompanyId" uuid ,
		"Remarks" character varying(250) 
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			PreviousPFGratuityEarnLeave."Id" , 
			PreviousPFGratuityEarnLeave."EmployeeId" , 
			PreviousPFGratuityEarnLeave."PFAmount" , 
			PreviousPFGratuityEarnLeave."GratuityAmount" , 
			PreviousPFGratuityEarnLeave."EarnLeaveAmount" , 
			PreviousPFGratuityEarnLeave."TillDate" , 
			PreviousPFGratuityEarnLeave."CompanyId" , 
			PreviousPFGratuityEarnLeave."Remarks" 
		 FROM employee."PreviousPFGratuityEarnLeaves" AS PreviousPFGratuityEarnLeave 
		 WHERE PreviousPFGratuityEarnLeave."IsDeleted" = False and PreviousPFGratuityEarnLeave."CompanyId" = companyId;
	END;
$BODY$
LANGUAGE plpgsql;

--select * from employee.GetPreviousPFGratuityEarnLeaveByCompanyId('ab5aeca2-7a4a-4a20-bb96-383e72e839dc');