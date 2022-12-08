CREATE OR REPLACE FUNCTION task.gettaskdetaillogbyemployeeid(employeeid uuid)
 RETURNS TABLE("Id" uuid, "TaskDetailId" uuid, "UpdateInfo" character varying, "DateUpdated" timestamp without time zone, "EmployeeId" uuid, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "SpendTime" timestamp without time zone)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
		    TaskDetailLog."Id",
		    TaskDetailLog."TaskDetailId",
		    TaskDetailLog."UpdateInfo",
		    TaskDetailLog."DateUpdated",
		    TaskDetailLog."EmployeeId",
		    TaskDetailLog."StartDate",
		    TaskDetailLog."EndDate",
		    TaskDetailLog."SpendTime"
		  --  e."FullName"  as "EmployeeName"
		 FROM Task."TaskDetailLogs"    AS TaskDetailLog 
		 WHERE   TaskDetailLog."IsDeleted" = false and TaskDetailLog."EmployeeId"  = employeeid
		 ;
	END;
$function$
;
