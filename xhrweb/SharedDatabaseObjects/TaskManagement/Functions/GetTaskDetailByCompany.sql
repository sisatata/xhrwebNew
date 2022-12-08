CREATE OR REPLACE FUNCTION task.gettaskdetailbycompany(companyid uuid)
 RETURNS TABLE("Id" uuid, "TaskTypeId" uuid, "TaskName" character varying, "TaskDescription" character varying, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "StatusId" integer, "ManagerId" uuid, "CompanyId" uuid, "ParentTaskId" uuid)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
		    TaskDetail."Id", 
		    TaskDetail."TaskTypeId" ,
			TaskDetail."TaskName",
			TaskDetail."TaskDescription",
			TaskDetail."StartDate",
			TaskDetail."EndDate",
			TaskDetail."StatusId",
			TaskDetail ."ManagerId" ,
			TaskDetail ."CompanyId" ,
			TaskDetail ."ParentTaskId" 
		 FROM Task."TaskDetails"   AS TaskDetail 
		 WHERE   TaskDetail."IsDeleted" = false and TaskDetail."CompanyId" = companyid
		 ;
	END;
$function$
;
