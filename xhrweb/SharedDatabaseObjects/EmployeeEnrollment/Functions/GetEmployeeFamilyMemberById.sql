CREATE OR REPLACE FUNCTION employee.GetEmployeeFamilyMemberById(id uuid)
RETURNS TABLE (
		"Id" uuid ,
		"EmployeeId" uuid ,
		"FamiliyMemberName" character varying(100) ,
		"DateOfBirth" date ,
		"EducationClass" character varying(150) ,
		"EducationalInstitute" character varying(150) ,
		"EducationYear" character varying(20) ,
		"RelationTypeId" uuid ,
		"IsDependant" boolean ,
		"IsEligibleForMedical" boolean ,
		"IsEligibleForEducation" boolean ,
		"IsDeleted" boolean ,
		"CompanyId" uuid
)
AS $BODY$
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
			EmployeeFamilyMember."CompanyId" 
		 FROM employee."EmployeeFamilyMembers" AS EmployeeFamilyMember 
		 WHERE EmployeeFamilyMember."IsDeleted" = False and EmployeeFamilyMember."Id" = id;
	END;
$BODY$
LANGUAGE plpgsql;

