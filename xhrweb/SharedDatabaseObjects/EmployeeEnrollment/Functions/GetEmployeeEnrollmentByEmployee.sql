CREATE OR REPLACE FUNCTION employee.getemployeeenrollmentbyemployee(employeeid uuid)
 RETURNS TABLE("Id" uuid, "EmployeeId" uuid, "JoiningDate" timestamp without time zone, "QuitDate" timestamp without time zone, "StatusId" smallint, "EmployeeStatusName" character varying, "PermanentDate" timestamp without time zone, "JobTypeId" uuid, "GradeId" uuid, "SubGradeId" uuid, "EmployeeTypeId" uuid, "EmployeeCategoryId" uuid, "ConfirmationId" uuid, "GenderId" uuid, "EmployeeImageUri" text, "SignatureUri" text, "LeaveTypeGroupName" character varying, "LeaveTypeGroupId" integer)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			EmployeeEnrollment."Id" , 
			EmployeeEnrollment."EmployeeId" , 
			EmployeeEnrollment."JoiningDate" , 
			EmployeeEnrollment."QuitDate" , 
			EmployeeEnrollment."StatusId" , 
			es."EmployeeStatusName" ,
			EmployeeEnrollment."PermanentDate" , 
			EmployeeEnrollment."JobTypeId" , 
			EmployeeEnrollment."GradeId" , 
			EmployeeEnrollment."SubGradeId" , 
			EmployeeEnrollment."EmployeeTypeId" , 
			EmployeeEnrollment."EmployeeCategoryId" , 
			EmployeeEnrollment."ConfirmationId" , 
			EmployeeEnrollment."GenderId"  ,
			EmployeeEnrollment."EmployeeImageUri",
			EmployeeEnrollment."EmployeeSignature" as "SignatureUri" ,
			ewcc."Value" as "LeaveTypeGroupName",
			ltg."Id" as "LeaveTypeGroupId"
		 FROM employee."EmployeeEnrollments" AS EmployeeEnrollment 
		 inner join employee."Employees" e  on EmployeeEnrollment."EmployeeId"  = e."Id" 
		 inner join employee."EmployeeStatuses" es on EmployeeEnrollment."StatusId" = es."StatusId" 
		 	and es."IsDeleted" = false and e."CompanyId" = es."CompanyId" 
		 	left join employee."EmployeeWiseCustomConfigurations" ewcc  on e."Id" = ewcc."EmployeeId" 
			and ewcc."Code" = 'LeaveGroup' 
		 left join leave."LeaveTypeGroups" ltg on ewcc."Value" = ltg."LeaveTypeGroupName"
		 WHERE EmployeeEnrollment."EmployeeId" = employeeId;
	END;
$function$
;