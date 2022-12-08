CREATE OR REPLACE FUNCTION employee.getemployeefamilymemberbyemployee(employeeid uuid)
 RETURNS TABLE("Id" uuid, "EmployeeId" uuid, "FamiliyMemberName" character varying, "DateOfBirth" timestamp, "EducationClass" character varying,
 "EducationalInstitute" character varying, "EducationYear" character varying, "RelationTypeId" uuid, "IsDependant" boolean,
 "IsEligibleForMedical" boolean, "IsEligibleForEducation" boolean, "IsDeleted" boolean, "CompanyId" uuid, "RelationTypeName" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			EmployeeFamilyMember."Id" , 
			EmployeeFamilyMember."EmployeeId" , 
			EmployeeFamilyMember."FamiliyMemberName" , 
			EmployeeFamilyMember."DateOfBirth" , 
			EmployeeFamilyMember."EducationClass" , 
			EmployeeFamilyMember."EducationalInstitute" , 
			EmployeeFamilyMember."EducationYear" , 
			EmployeeFamilyMember."RelationTypeId" , 
			EmployeeFamilyMember."IsDependant" , 
			EmployeeFamilyMember."IsEligibleForMedical" , 
			EmployeeFamilyMember."IsEligibleForEducation" , 
			EmployeeFamilyMember."IsDeleted" , 
			EmployeeFamilyMember."CompanyId" ,
			reltyp."LookUpTypeName" as "RelationTypeName"
		 FROM employee."EmployeeFamilyMembers" AS EmployeeFamilyMember 
		 inner join main."CommonLookUpTypes" reltyp on EmployeeFamilyMember."RelationTypeId" = reltyp."Id" 
		 WHERE EmployeeFamilyMember."IsDeleted" = False and EmployeeFamilyMember."EmployeeId" = employeeId;
	END;
$function$
;

--DROP FUNCTION employee.getemployeefamilymemberbyemployee(uuid);

--select * from employee.getemployeefamilymemberbyemployee('2a8ea0cf-ffa3-4859-afa3-912d5e8817cb')