CREATE OR REPLACE FUNCTION task.gettaskdetailbytaskdetailid(taskdetailid uuid)
 RETURNS TABLE("Id" uuid, "TaskTypeId" uuid, "TaskName" character varying, "TaskDescription" character varying, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "StatusId" integer, "ManagerId" uuid, "CreatedBy" character varying, "FullName" character varying, "CompanyId" uuid, "ParentTaskId" uuid, "TaskCategoryName" character varying)
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
			TaskDetail."CreatedBy",
			e."FullName",
			TaskDetail ."CompanyId" ,
			TaskDetail ."ParentTaskId" ,
			tc."TaskCategoryName" 
		 FROM Task."TaskDetails"   AS TaskDetail 
		  left  join   employee."Employees" e on e."Id" =TaskDetail ."ManagerId" 
		 inner join task."TaskCategories" tc on tc."Id" = TaskDetail."TaskTypeId"
		 WHERE TaskDetail."IsDeleted" = false and cast(TaskDetail."Id" as uuid)  = taskdetailid 

		 ;
	END;
$function$
;
