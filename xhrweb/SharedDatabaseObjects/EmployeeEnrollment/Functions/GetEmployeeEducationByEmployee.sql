CREATE OR REPLACE FUNCTION employee.getemployeeeducationbyemployee(employeeid uuid)
 RETURNS TABLE("Id" uuid, "EmployeeId" uuid, "EducationalDegreeId" uuid, "EducationalInstituteId" uuid, "Session" character varying, "PassingYear" character varying, "BoardorUniversity" character varying, "Result" character varying, "ResultType" character varying, "OutOf" numeric, "IsDeleted" boolean, "CompanyId" uuid, "EducationalDegreeName" character varying, "EducationalInstituteName" character varying)
 LANGUAGE plpgsql
AS $function$
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
			EmployeeEducation."CompanyId",
			deg."LookUpTypeName" as "EducationalDegreeName",
			inst."LookUpTypeName" as "EducationalInstituteName"
		 FROM employee."EmployeeEducations" AS EmployeeEducation 
		 inner join main."CommonLookUpTypes" deg on EmployeeEducation."EducationalDegreeId" = deg ."Id" 
		 inner join main."CommonLookUpTypes" inst on EmployeeEducation."EducationalInstituteId" = inst."Id" 
		 WHERE EmployeeEducation."IsDeleted" = False 
		 and EmployeeEducation."EmployeeId" = employeeId;
	END;
$function$
;

 --DROP FUNCTION employee.getemployeeeducationbyemployee(uuid) ;
