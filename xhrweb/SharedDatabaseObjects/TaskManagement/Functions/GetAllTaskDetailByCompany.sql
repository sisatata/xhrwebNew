CREATE OR REPLACE FUNCTION task.getalltaskdetailbycompany(companyid uuid)
 RETURNS TABLE("Id" uuid, "TaskTypeId" uuid, "TaskName" character varying, "TaskDescription" character varying, "Progress" integer, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "StatusId" integer, "AssigneeId" uuid, "CreatedBy" character varying, "TaskCreator" uuid, "CompanyId" uuid, "ParentTaskId" uuid, "TaskCategoryName" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
		    TaskDetail."Id", 
		    TaskDetail."TaskTypeId" ,
			TaskDetail."TaskName",
			TaskDetail."TaskDescription",
			TaskDetail."Progress",
			TaskDetail."StartDate",
			TaskDetail."EndDate",
			TaskDetail."StatusId",
			TaskDetail."AssigneeId" ,
			TaskDetail."CreatedBy",
			TaskDetail."TaskCreator",
			TaskDetail ."CompanyId" ,
			TaskDetail ."ParentTaskId" ,
			tc."TaskCategoryName" 
		 FROM Task."TaskDetails"   AS TaskDetail 
		 inner join task."TaskCategories" tc on tc."Id" = TaskDetail."TaskTypeId"
		 WHERE TaskDetail."IsDeleted" = false and TaskDetail."CompanyId" = companyid
		 and TaskDetail."IsDeleted"  = false;
	
		
		 
	END;
$function$
;
