CREATE OR REPLACE FUNCTION leave.getleavetypegroupbycompanyid(companyid uuid)
 RETURNS TABLE("Id" integer, "LeaveTypeGroupName" character varying, "LeaveTypeGroupNameLC" character varying, "CompanyId" uuid, "IsDeleted" boolean)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			LeaveTypeGroup."Id" , 
			LeaveTypeGroup."LeaveTypeGroupName" , 
			LeaveTypeGroup."LeaveTypeGroupNameLC" , 
			LeaveTypeGroup."CompanyId" , 
			LeaveTypeGroup."IsDeleted" 
		 from leave."LeaveTypeGroups" AS LeaveTypeGroup 
		 WHERE LeaveTypeGroup."IsDeleted" = False and LeaveTypeGroup."CompanyId" = companyid;
	END;
$function$
;
