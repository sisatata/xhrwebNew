CREATE OR REPLACE FUNCTION access.getrolesbyuser(employeeid uuid)
 RETURNS TABLE("Id" uuid, "EmployeeName" character varying, "UserId" uuid, "RoleId" uuid, "ItemName" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT e."Id" as "EmployeeId",
		e."FullName" as "EmployeeName" ,
		e."LoginId"  as "UserId",
		cast(anur."RoleId" as uuid) as "RoleId",
		anr."Name" as "ItemName"
			
		 FROM employee."Employees" e inner join 
		 "access"."AspNetUserRoles" anur on cast(anur."UserId" as uuid) =  e."LoginId"   inner join 
		 "access"."AspNetRoles" anr on anr."Id" = anur."RoleId" 
		 where e."Id" = employeeid and e."IsDeleted" =false;
		 
		
	END;
$function$
;
