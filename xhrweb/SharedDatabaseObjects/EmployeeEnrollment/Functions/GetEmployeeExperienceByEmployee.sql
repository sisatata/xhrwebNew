CREATE OR REPLACE FUNCTION employee.GetEmployeeExperienceByEmployee(employeeId uuid)
RETURNS TABLE (
		"Id" uuid ,
		"EmployeeId" uuid ,
		"CompanyName" character varying(150) ,
		"Designation" character varying(150) ,
		"FunctionalDesignation" character varying(150) ,
		"Department" character varying(150) ,
		"Responsibilities" character varying(500) ,
		"CompanyAddress" character varying(150) ,
		"CompanyContactNo" character varying(20) ,
		"CompanyMobile" character varying(20) ,
		"CompanyEmail" character varying(150) ,
		"CompanyWeb" character varying(50) ,
		"FromDate" timestamp without time zone ,
		"ToDate" timestamp without time zone ,
		"TilDate" boolean ,
		"IsDeleted" boolean ,
		"CompanyId" uuid 
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			EmployeeExperience."Id" , 
			EmployeeExperience."EmployeeId" , 
			EmployeeExperience."CompanyName" , 
			EmployeeExperience."Designation" , 
			EmployeeExperience."FunctionalDesignation" , 
			EmployeeExperience."Department" , 
			EmployeeExperience."Responsibilities" , 
			EmployeeExperience."CompanyAddress" , 
			EmployeeExperience."CompanyContactNo" , 
			EmployeeExperience."CompanyMobile" , 
			EmployeeExperience."CompanyEmail" , 
			EmployeeExperience."CompanyWeb" , 
			EmployeeExperience."FromDate" , 
			EmployeeExperience."ToDate" , 
			EmployeeExperience."TilDate" , 
			EmployeeExperience."IsDeleted" , 
			EmployeeExperience."CompanyId" 
		 FROM employee."EmployeeExperiences" AS EmployeeExperience 
		 WHERE EmployeeExperience."IsDeleted" = False AND EmployeeExperience."EmployeeId" = employeeId;
	END;
$BODY$
LANGUAGE plpgsql;