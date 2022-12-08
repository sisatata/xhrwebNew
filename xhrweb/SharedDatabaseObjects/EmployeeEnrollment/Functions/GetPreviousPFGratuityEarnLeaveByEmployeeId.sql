CREATE OR REPLACE FUNCTION employee.GetPreviousPFGratuityEarnLeaveByEmployeeId(employeeId uuid)
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
		 WHERE PreviousPFGratuityEarnLeave."IsDeleted" = False and PreviousPFGratuityEarnLeave."EmployeeId" = employeeId;
	END;
$BODY$
LANGUAGE plpgsql;
