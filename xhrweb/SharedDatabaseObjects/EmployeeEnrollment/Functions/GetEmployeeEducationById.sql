CREATE OR REPLACE FUNCTION employee.GetEmployeeEducationById(
educationId uuid )
RETURNS TABLE (
		"Id" uuid ,
		"EmployeeId" uuid ,
		"EducationalDegreeId" uuid ,
		"EducationalInstituteId" uuid ,
		"Session" character varying(20) ,
		"PassingYear" character varying(20) ,
		"BoardorUniversity" character varying(150) ,
		"Result" character varying(30) ,
		"ResultType" character varying(10) ,
		"OutOf" Decimal ,
		"IsDeleted" boolean ,
		"CompanyId" uuid 
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			EmployeeEducation."Id" , 
			EmployeeEducation."EmployeeId" , 
			EmployeeEducation."EducationalDegreeId" , 
			EmployeeEducation."EducationalInstituteId" , 
			EmployeeEducation."Session" , 
			EmployeeEducation."PassingYear" , 
			EmployeeEducation."BoardorUniversity" , 
			EmployeeEducation."Result" , 
			EmployeeEducation."ResultType" , 
			EmployeeEducation."OutOf" , 
			EmployeeEducation."IsDeleted" , 
			EmployeeEducation."CompanyId"
		 FROM employee."EmployeeEducations" AS EmployeeEducation 
		 WHERE EmployeeEducation."IsDeleted" = False 
		 and EmployeeEducation."Id" = educationId;
	END;
$BODY$
LANGUAGE plpgsql;

