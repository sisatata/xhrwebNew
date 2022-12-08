CREATE OR REPLACE FUNCTION employee.getemployeeinformationbyuserid(userid character varying)
 RETURNS TABLE("EmployeeId" uuid, "EmployeeName" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT e."Id" as "EmployeeId",
		e."FullName" as "EmployeeName" 
		 FROM "access"."AspNetUsers" anu
		 inner join employee."Employees" e on cast(anu."Id" as uuid) = e."LoginId" 
		 where anu."Id" = userid and e."IsDeleted" =false;
		 
		
	END;
$function$
;
